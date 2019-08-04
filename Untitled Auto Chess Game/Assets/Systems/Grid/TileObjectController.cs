using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GridSystem
{
    [RequireComponent(typeof(TileObject))]
    class TileObjectController : MonoBehaviour
    {
        TileObject _tileObject;
        [SerializeField] [HideInInspector] Grid _grid;
        [SerializeField] Movement _movement;
        public UnityEvent BeginMove = new UnityEvent();
        public UnityEvent EndMove = new UnityEvent();
        //public IntIntEvent Move = new IntIntEvent();

        private void Awake()
        {
            _tileObject = GetComponent<TileObject>();
            _grid = FindObjectOfType<Grid>();
        }

        private void OnEnable()
        {
            _grid = FindObjectOfType<Grid>();
        }

        // private void OnMove(int ix, int iy) => Move?.Invoke(ix, iy);

        public void AttemptMove(DirectionEnum direction)
        {
            OnBeginMove();
            StartCoroutine(_movement.MoveAdjacent(_tileObject, 1, direction, OnEndMove));
        }

        private void OnBeginMove() => BeginMove?.Invoke();
        private void OnEndMove() => EndMove?.Invoke();


#if UNITY_EDITOR

        [Header("DEBUG")]
        public bool moveRandom = false;
        //public bool randomDirection = false;
        //public DirectionEnum direction;
        //public bool randomStops = false;

        private void Start()
        {
            if (moveRandom)
                MoveTest();
        }


        public void MoveTest()
        {
            var arr = Enum.GetValues(typeof(DirectionEnum));
            DirectionEnum rand;
            //if (randomDirection)
                rand = (DirectionEnum)arr.GetValue(UnityEngine.Random.Range(0, arr.Length));
            //else
                //rand = direction;

            AttemptMove(rand);
        }

        #endif
    }

    public class IntIntEvent : UnityEvent<int, int> { }
}
