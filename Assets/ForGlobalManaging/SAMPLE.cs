using System;
using System.Collections.Generic;
using UnityEngine;

public class SAMPLE
{
    class Player : CharacterBase, IOnGameStates, IOnEnemyDie, ITransformGettable
    {
        public Transform _transform => transform;
        public void OnEnemyDie(){//logic
        }
        public void OnGameOver(){//logic
        }
        public void OnGamePause(){//logic
        }
        public void OnGameRunning(){//Update logic
        }
        public void OnGameStart(params object[] parameter){//Start logic
        }
        public void OnStageOver(){//logic
        }
        public void OnStageStart(){//logic
        }
        public override int damage => 0/*for example*/;
        public override void BeAttacked(int damage){//logic
        }
    }

    class EnemySpawner : MonoBehaviour, IOnGameStates
    {
        ITransformGettable player;
        IOnEnemyDie dieDependency;
        GameObject prefab;
        List<IOnGameStates> enemies;
        public void OnGameOver(){
            Iterate(enemy => () => enemy.OnGameOver());
        }
        public void OnGamePause(){
            Iterate(enemy => () => enemy.OnGamePause());
        }
        public void OnGameRunning(){
            Iterate(enemy => () => enemy.OnGameRunning());
        }
        public void OnGameStart(params object[] parameter)
        {
            foreach (var param in parameter)
            {
                if (param.GetType() is ITransformGettable instance1)
                    player = instance1;
                if (param.GetType() is IOnEnemyDie instance2)
                    dieDependency = instance2;
            }
            prefab = Resources.Load<GameObject>("");
            Spawn();
            foreach (IOnGameStates enemy in enemies)
                enemy.OnGameStart(player, dieDependency);
        }
        public void OnStageOver(){
            Iterate(enemy => () => enemy.OnStageOver());
        }
        public void OnStageStart(){
            Iterate(enemy => () => enemy.OnStageStart());
        }
        void Spawn(){
            enemies.Add(Instantiate(prefab).GetComponent<IOnGameStates>());
        }
        void Iterate(Func<IOnGameStates, Action> Invoke){
            foreach (IOnGameStates enemy in enemies)
                Invoke(enemy);
        }
    }

    class Enemy : CharacterBase, IOnGameStates
    {
        ITransformGettable player;
        IOnEnemyDie dieDependency;
        public void OnGameOver(){//logic
        }
        public void OnGamePause(){//logic
        }
        public void OnGameRunning(){//Update logic
        }
        public void OnGameStart(params object[] parameter){
            foreach (var param in parameter){
                if (param.GetType() is ITransformGettable instance1)
                    player = instance1;
                if (param.GetType() is IOnEnemyDie instance2)
                    dieDependency = instance2;
            }
            GetComponent<Animator>().GetBehaviour<EnemyMove>().player = player._transform;
            //Start logic
        }
        public void OnStageOver(){//logic
        }
        public void OnStageStart(){//logic
        }
        public override int damage => 0/*for example*/;
        public override void BeAttacked(int damage){//logic
            dieDependency.OnEnemyDie();
        }
    }

    class EnemyMove : StateMachineBehaviour{
        public Transform player { get; set; }
        //logic
    }

    class Camera : MonoBehaviour, IOnGameStates
    {
        ITransformGettable player;
        bool gamePause;
        public void OnEnemyDie(){//logic
        }
        public void OnGameOver(){//logic
        }
        public void OnGamePause(){
            gamePause = true;
        }
        public void OnGameRunning(){//Update logic
        }
        public void OnGameStart(params object[] parameter){
            foreach (var param in parameter)
                if (param.GetType() is ITransformGettable instance){
                    player = instance;
                    break;
                }
            //Start logic
        }
        public void OnStageOver(){//logic
        }
        public void OnStageStart(){//logic
        }
        void LateUpdate(){
            if (gamePause)
                return;
            transform.position = player._transform.position + Vector3.one/*for example*/;
        }
    }
}
