using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Giá trị âm lượng mặc định
    public static float DefaultBgmVolume = 1;
    public static float DefaultSfxVolume = 1;
    public static float DefaultVoiceVolume = 1;
    public static float DefaultBgmVolumeScale = 1f;
    public static float DefaultSfxVolumeScale = 1;
    public static float DefaultVoiceVolumeScale = 1;

    // Giá trị âm lượng đang được lưu trữ
    private static float bgmVolume = -1;
    private static float sfxVolume = -1;
    private static float voiceVolume = -1;

    private static float bgmVolumeScale = 1;

    // Khóa cho các thiết lập âm lượng trong PlayerPrefs
    public static string PREF_SFX_VOLUME = "SFX_VOLUME";
    public static string PREF_BGM_VOLUME = "BGM_VOLUME";

    // Tên các đoạn âm thanh
    public const string upgrade = "upgrade";
    public const string upgrade_sucess = "sound_nangcapxongphong";
    public const string level_Up = "level_up4";

    // Các thuộc tính để lấy và thiết lập âm lượng
    public static float BgmVolume
    {
        get
        {
            if (bgmVolume == -1)
            {
                bgmVolume = PlayerPrefs.GetFloat("BGM_VOLUME", DefaultBgmVolume);
            }

            return bgmVolume;
        }

        set
        {
            bgmVolume = value;
            instance.m_BgmSource.volume = bgmVolume * DefaultBgmVolumeScale * bgmVolumeScale;
        }
    }

    public static float SfxVolume
    {
        get
        {
            if (sfxVolume == -1)
            {
                sfxVolume = PlayerPrefs.GetFloat("SFX_VOLUME", DefaultSfxVolume);
            }

            return sfxVolume;
        }

        set
        {
            sfxVolume = value;
        }
    }

    public static float VoiceVolume
    {
        get
        {
            if (voiceVolume == -1)
            {
                voiceVolume = PlayerPrefs.GetFloat("VOICE_VOLUME", DefaultVoiceVolume);
            }

            return voiceVolume;
        }

        set
        {
            voiceVolume = value;
        }
    }

    // Phương thức để thay đổi trạng thái âm lượng nhạc nền
    public void ChangeBGSound(bool state)
    {
        if (state)
        {
            BgmVolume = 0.5f;
        }
        else
        {
            BgmVolume = 0;
        }
    }

    // Phương thức để thay đổi trạng thái âm lượng hiệu ứng âm thanh
    public void ChangeSFXSound(bool state)
    {
        if (state)
        {
            sfxVolume = 1f;
        }
        else
        {
            sfxVolume = 0;
        }
    }

    // Lưu các thiết lập âm lượng vào PlayerPrefs
    public static void Save()
    {
        PlayerPrefs.SetFloat("BGM_VOLUME", BgmVolume);
        PlayerPrefs.SetFloat("SFX_VOLUME", SfxVolume);
        PlayerPrefs.SetFloat("VOICE_VOLUME", VoiceVolume);
        PlayerPrefs.Save();
    }

    // Các trường serialize cho đường dẫn âm thanh và nguồn âm thanh
    [SerializeField] private string m_BgmPath = "Sounds/Bgm";
    [SerializeField] private string m_SfxPath = "Sounds/Sfx";
    [SerializeField] private string m_VoicePath = "Sounds/Voice";
    [SerializeField] private string m_ButtonTapSfx = "sfx_button_tap";
    [SerializeField] private AudioSource m_BgmSource;
    [SerializeField] private AudioSource m_SfxSource;
    [SerializeField] private AudioSource m_VoiceSource;

    // Đối tượng singleton instance
    public static AudioManager instance { get; protected set; }

    // Dictionaries để cache các âm thanh và giới hạn tần suất phát
    private Dictionary<string, float> soundFxLimit = new Dictionary<string, float>();
    private Dictionary<string, AudioClip> m_AudioDict = new Dictionary<string, AudioClip>();
    private bool m_PauseBgm;

    // Các thuộc tính để lấy đường dẫn và nguồn âm thanh
    public string sfxPath
    {
        get { return m_SfxPath; }
    }

    public string bgmPath
    {
        get { return m_BgmPath; }
    }

    public string voicePath
    {
        get { return m_VoicePath; }
    }

    public AudioSource voiceSource
    {
        get { return m_VoiceSource; }
    }

    // Phương thức để phát hiệu ứng âm thanh khi bấm nút
    public void PlayButtonTapSfx()
    {
        PlaySfx(m_ButtonTapSfx, 1);
    }

    // Phương thức để phát ngẫu nhiên một hiệu ứng âm thanh từ một mảng
    public void PlaySfx(string[] array, float volumeScale = 1f)
    {
        if (array.Length > 0)
            PlaySfx(array[UnityEngine.Random.Range(0, array.Length)], volumeScale, true);
    }

    public void PlaySfx(AudioClip[] array, float volumeScale = 1f)
    {
        if (array.Length > 0)
            PlaySfx(array[UnityEngine.Random.Range(0, array.Length)], volumeScale);
    }

    public void PlaySfx(AudioClip audioClip, float volumeScale = 1f)
    {
        m_SfxSource.PlayOneShot(audioClip, SfxVolume * DefaultSfxVolumeScale * volumeScale);
    }

    // Phương thức để phát một hiệu ứng âm thanh với các tùy chọn khác nhau
    public void PlaySfx(string audioName, float volumeScale = 1f, bool oneShot = true, bool isLoop = false, float delay = 0, Action actionMoveCam = null)
    {
        if (!CanPlaySound(audioName)) return;

        StartCoroutine(CoroutinePlaySFX(audioName, volumeScale, oneShot, isLoop, delay, actionMoveCam));
    }

    // Kiểm tra xem có thể phát hiệu ứng âm thanh dựa trên giới hạn tần suất phát
    private bool CanPlaySound(string audioName)
    {
        if (soundFxLimit.ContainsKey(audioName))
        {
            if (Time.time - soundFxLimit[audioName] >= .5f)
            {
                soundFxLimit[audioName] = Time.time;
                return true;
            }
            return false;
        }
        else soundFxLimit.Add(audioName, Time.time);
        return true;
    }

    private bool isPlayingUpgrade;

    // Coroutine để phát hiệu ứng âm thanh với các tùy chọn delay và khác
    private IEnumerator CoroutinePlaySFX(string audioName, float volumeScale = 1f, bool oneShot = true, bool isLoop = false, float delay = 0, Action actionMoveCam = null)
    {
        if (isPlayingUpgrade && audioName == upgrade)
        {
            actionMoveCam?.Invoke();
        }
        else
        {
            if (audioName == upgrade)
            {
                isPlayingUpgrade = true;
                actionMoveCam?.Invoke();
            }
            yield return new WaitForSeconds(delay);
            CacheClip(m_SfxPath, audioName);

            m_SfxSource.volume = SfxVolume * DefaultSfxVolumeScale * volumeScale;
            if (oneShot)
            {
                m_SfxSource.PlayOneShot(m_AudioDict[audioName], m_SfxSource.volume);
            }
            if (isLoop)
            {
                m_SfxSource.loop = isLoop;
                m_SfxSource.clip = m_AudioDict[audioName];
                m_SfxSource.Play();
            }
            if (audioName == upgrade)
            {
                isPlayingUpgrade = false;
            }
        }
    }

    // Phương thức để dừng phát hiệu ứng âm thanh
    public void StopSfx()
    {
        m_SfxSource.Stop();
    }

    // Phương thức để lấy độ dài của một hiệu ứng âm thanh
    public float GetLength(string audioName)
    {
        return (m_AudioDict.ContainsKey(audioName) ? m_AudioDict[audioName].length : 0);
    }


    public float PlayVoice(AudioClip audioClip, float volumeScale = 1f)
    {
        m_VoiceSource.volume = VoiceVolume * DefaultVoiceVolume * volumeScale;
        m_VoiceSource.PlayOneShot(audioClip, m_VoiceSource.volume);
        return audioClip.length;
    }

    public void PlayVoice(string audioName, float volumeScale = 1f, bool playOneShot = false)
    {
        CacheClip(m_VoicePath, audioName);

        m_VoiceSource.volume = VoiceVolume * DefaultVoiceVolume * volumeScale;
        if (playOneShot)
        {
            m_VoiceSource.PlayOneShot(m_AudioDict[audioName], m_VoiceSource.volume);
        }
        else
        {
            m_VoiceSource.clip = m_AudioDict[audioName];
            m_VoiceSource.Play();
        }
    }

    // Phương thức để thiết lập pitch của giọng nói
    public void SetVoicePitch(float pitch)
    {
        m_VoiceSource.pitch = pitch;
    }

    // Phương thức để phát nhạc nền (BGM)
    public void PlayBgm(string audioName, float volumeScale = 1f)
    {
        bgmVolumeScale = volumeScale;
        m_BgmSource.volume = BgmVolume * DefaultBgmVolumeScale * bgmVolumeScale;

        bool currentClipNull = (m_BgmSource.clip == null);
        bool sameCurrentClip = (!currentClipNull && m_BgmSource.clip.name == audioName);

        if (sameCurrentClip)
        {
            if (!m_BgmSource.isPlaying)
            {
                m_BgmSource.Play();
            }
        }
        else
        {
            if (!currentClipNull)
            {
                StopBgm(true);
            }
            m_BgmSource.clip = Resources.Load<AudioClip>(System.IO.Path.Combine(m_BgmPath, audioName));
            m_BgmSource.Play();
        }
    }

    // Phương thức để dừng phát nhạc nền (BGM)
    public void StopBgm(bool clearClip = false)
    {
        if (m_BgmSource.isPlaying)
        {
            m_BgmSource.Stop();
        }

        if (clearClip && m_BgmSource.clip != null)
        {
            Resources.UnloadAsset(m_BgmSource.clip);
            m_BgmSource.clip = null;
        }
    }

    // Thuộc tính để tạm dừng hoặc tiếp tục phát nhạc nền (BGM)
    public bool pauseBgm
    {
        set
        {
            m_PauseBgm = value;

            if (m_PauseBgm)
            {
                m_BgmSource.Pause();
            }
            else
            {
                m_BgmSource.UnPause();
            }
        }
    }

    // Phương thức để cache một AudioClip bằng đối tượng clip và tên âm thanh
    public void CacheClip(AudioClip clip, string audioName)
    {
        if (!m_AudioDict.ContainsKey(audioName))
        {
            m_AudioDict.Add(audioName, clip);
        }
    }

    // Phương thức để cache một AudioClip từ đường dẫn và tên âm thanh
    public void CacheClip(string path, string audioName)
    {
        /*
        if (m_AudioDict.Count > 10) // Làm rỗng cache nếu quá lớn
        {
            foreach (var item in m_AudioDict)
            {
                Resources.UnloadAsset(item.Value);
            }
            m_AudioDict.Clear();
        }
        */
        if (!m_AudioDict.ContainsKey(audioName))
        {
            AudioClip clip = Resources.Load<AudioClip>(System.IO.Path.Combine(path, audioName));
            if (clip == null) { Debug.LogError("Không tìm thấy AudioClip: " + System.IO.Path.Combine(path, audioName)); }
            m_AudioDict.Add(audioName, clip);
        }
    }

    // Phương thức để cache các hiệu ứng âm thanh từ mảng
    public void CacheSfx(string[] array)
    {
        foreach (var item in array)
        {
            CacheSfx(item);
        }
    }

    // Phương thức để cache một hiệu ứng âm thanh
    public void CacheSfx(string audioName)
    {
        CacheClip(m_SfxPath, audioName);
    }

    // Phương thức để xóa cache clip
    public void ClearCacheClip(string audioName, bool unloadClip)
    {
        if (m_AudioDict.ContainsKey(audioName))
        {
            if (unloadClip)
            {
                Resources.UnloadAsset(m_AudioDict[audioName]);
            }
            m_AudioDict.Remove(audioName);
        }
    }

    // Phương thức để làm rỗng toàn bộ cache clip
    public void ClearCacheClip()
    {
        foreach (var item in m_AudioDict)
        {
            Resources.UnloadAsset(item.Value);
        }
        m_AudioDict.Clear();
    }

    // Phương thức để làm mờ dần âm thanh (để trống)
    public void Fadeout()
    {
    }

    // Phương thức Awake để khởi tạo instance và thiết lập không bị phá hủy khi chuyển scene
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Phương thức Start để bắt đầu phát nhạc nền và đặt tùy chọn âm thanh
    private void Start()
    {
        StopAllCoroutines();
        PlayBgm("BGM", 0.5f);
        //PlaySfx("StartCar", .4f);


        //ChangeBGSound(Game.Data.Load<UserData>().musicOn);
        //ChangeSFXSound(Game.Data.Load<UserData>().soundOn);
    }

    // Các phương thức bổ sung để phát âm thanh khi tương tác
    public void ClickButton()
    {
        PlaySfx("Button", 1);
    }
    public void ClickLetter()
    {
        PlaySfx("LetterPick", 1);
    }
    public void SoundWrong()
    {
        PlaySfx("Wrong", 1);
    }
    public void SoundLose()
    {
        PlaySfx("Lose", 1);
    }
    public void SoundWin()
    {
        PlaySfx("Win", 1);
    }
    public void SoundRewardStar()
    {
        PlaySfx("StarCollect", 1);
    }
    public void SoundLuckyWheel()
    {
        PlaySfx("LuckyWheel", 1);
    }
    public void SoundLuckyWheelResult()
    {
        PlaySfx("LuckyWheelResult", 1);
    }
    public void SoundRewardCoin()
    {
        PlaySfx("Coin", 1);
    }
    public void CloseButton()
    {
        PlaySfx("CloseBtn", 1);
    }

}