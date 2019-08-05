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

        private void Awake()
        {
            _tileObject = GetComponent<TileObject>();
            _grid = FindObjectOfType<Grid>();
        }

        private void OnEnable()
        {
            _grid = FindObjectOfType<Grid>();
        }

        public void AttemptMove(DirectionEnum direction)
        {
            OnBeginMove();
            StartCoroutine(_movement.MoveAdjacent(_tileObject, 1, direction, OnEndMove));
        }

        public void Turn(DirectionEnum dir)
        {
            _movement.Turn(_tileObject, dir);
        }

        private void OnBeginMove() => BeginMove?.Invoke();
        private void OnEndMove() => EndMove?.Invoke();


#if UNITY_EDITOR

        [Header("DEBUG")]
        public bool moveRandom = false;
        public bool atkRandom;

        float counter = 1;

        private void Start()
        {
            if (moveRandom)
                MoveTest();
            else if (atkRandom)
            {
                FindTargetAttackTest();
                GetComponent<Attackable>().Death.AddListener(() => Destroy(gameObject));
            }
        }

        public void MoveTest()
        {
            if (!moveRandom)
                return;
            var arr = Enum.GetValues(typeof(DirectionEnum));
            DirectionEnum rand;
            rand = (DirectionEnum)arr.GetValue(UnityEngine.Random.Range(0, arr.Length));

            AttemptMove(rand);
        }

        public void FindTargetAttackTest()
        {
            if (!atkRandom)
                return;

            RaycastHit[] infos;
            RaycastHit info = new RaycastHit();
            infos = Physics.BoxCastAll(new Vector3(5, 10, 5), new Vector3(10, 1, 10), Vector3.down, Quaternion.identity, 20, 1024);
            var min = 200f;
            foreach (var hit in infos)
            {
                if (hit.transform.gameObject != gameObject)
                {
                    if ((hit.point - transform.position).magnitude < min)
                    {
                        min = (hit.point - transform.position).magnitude;
                        info = hit;
                    }
                }
            }

            var self = _tileObject;
            var target = info.transform.GetComponent<TileObject>();

            if (TileObject.MaxDistance(self, target) == 1)
            {
                _movement.TurnTo(self, target);
                Attack(target.GetComponent<Attackable>());
                return;
            }

            var dedu = target.transform.position - self.transform.position;
            var dir = Direction.FromV2(new Vector2(dedu.x, dedu.z));

            AttemptMove(dir);
        }

        public void Attack(Attackable target)
        {
            StartCoroutine(GetComponent<Attacker>().Attack(target));
        }
#endif
    }
}
