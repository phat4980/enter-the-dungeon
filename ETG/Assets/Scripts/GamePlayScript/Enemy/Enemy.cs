using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy{
    public abstract class Enemy : MonoBehaviour
    {
        #region General attribute
        [Tooltip("Auto set target")]
        public GameObject target;
        public PathFireManager pathFireManager;
        public List<GameObject> itemsToSpawn;
        //[SerializeField] protected float timer = 0;
        //public float shootingTime;
        public bool canCollision;
        #endregion

        [Header("Attributes user for fire")]
        #region Attribute use for fire
        [Tooltip("Spawn bullet position in enemy")]
        public Transform spawnBullet; // previous name : shootingPoint
        public GameObject projectilePrefab;
        public float projectileSpeed;
        public enum TypeBullet
        {
            CircleType,
            None
        }
        public TypeBullet typeBullet;
        [SerializeField]protected GameObject owner;
        #endregion

        [Header("Attributes for animation, state")]
        #region Attribute use for state
        public GameObject weapon;
        public GameObject hand;

        public float HP;
        public Animator animator;
        public StateManager stateManager;
        protected enum side
        {
            L, // Left side
            R, // Right side 
            O,
            U, // Back
            D  // Front
        }

        [SerializeField] protected string curVerSide, curHoriSide;
        [SerializeField] private Vector3 targetPos;
        [SerializeField] protected NavMeshAgent agent;
        #endregion 

        private void Awake()
        {
            pathFireManager = new PathFireManager();
            owner = gameObject;
            #region State
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            stateManager = new StateManager(new Spawn(), animator);
            stateManager.EnterState();
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            SetAgentPos(targetPos);
            WeaponLookAtPLayer();
            CompareTargetPositionToAgent();
            SetDir();
            if (agent.velocity != Vector3.zero && HP > 0) stateManager.SwithcState(new MoveState());
            else if (agent.velocity == Vector3.zero && HP > 0) stateManager.SwithcState(new IdleState());
            if (HP <= 0) Die();
        }

        private void SetAgentPos(Vector3 position)
        {
            var agentDrift = 0.0001f;
            var driftPos = position + (Vector3)(agentDrift * Random.insideUnitCircle);
            agent.SetDestination(driftPos);
        }
        private void WeaponLookAtPLayer()
        {
            if (weapon != null)
            {
                Vector3 vectorToTarget = target.transform.position - hand.transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                hand.GetComponent<Rigidbody2D>().rotation = angle;
            }
        }
        private void ChangeSideHand(int i) // Just 0 to turn to left and 1 to turn to right
        {
            if (i == 0) // Right - rotation to left
            {
                hand.transform.localScale = new Vector3(1, -1, 1);
                hand.transform.localPosition = new Vector3(-0.5f, -0.35f, 0);
            }
            if (i == 1) // Left  - rotation to right
            {
                hand.transform.localScale = new Vector3(1, 1, 1);
                hand.transform.localPosition = new Vector3(0.5f, -0.35f, 0);
            }
        }
        private void CompareTargetPositionToAgent()
        {
            if (targetPos.x > transform.position.x + 0.5f)
            {
                if (weapon != null)
                {
                    ChangeSideHand(1);
                }
                curHoriSide = side.R.ToString();
            }
            if (targetPos.x < transform.position.x - 0.5f)
            {
                if (weapon != null)
                {
                    ChangeSideHand(0);
                }
                curHoriSide = side.L.ToString();
            }
            if (targetPos.x <= transform.position.x + 0.5f && targetPos.x >= transform.position.x - 0.5f)
            {
                if (weapon != null)
                {
                    ChangeSideHand(1);
                }
                curHoriSide = side.O.ToString();
            }
            if (targetPos.y > transform.position.y + 0.5f) curVerSide = side.U.ToString();
            if (targetPos.y < transform.position.y + 0.5f) curVerSide = side.D.ToString();
            if (targetPos.y <= transform.position.y + 0.5f && targetPos.y >= transform.position.y - 0.5f) curVerSide = side.O.ToString();
        }

        private void SetDir()
        {
            if (animator.parameterCount == 1)
            {
                if (animator.GetParameter(0).name == "ver") stateManager.setDirection("O",curVerSide);
                if (animator.GetParameter(0).name == "hori") stateManager.setDirection(curHoriSide, "0");
            }
            if (animator.parameterCount == 2) stateManager.setDirection(curHoriSide,curVerSide);
        }

        public abstract void Fire();
        public void Die()
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            stateManager.SwithcState(new Die());
            if (hand != null) hand.SetActive(false);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                spawnAction();
                Destroy(gameObject, 3f);
                agent.isStopped = true;
                this.enabled = false;
            }
        }
        private void spawnAction()
        {
            int ran = Random.Range(4, 7);
            for (int i = 0; i < ran; i++)
            {
                int ranIndex = Random.Range(0, itemsToSpawn.Count - 1);
                GameObject item = Instantiate(itemsToSpawn[ranIndex], transform.position, Quaternion.identity);
                Vector2 randomDirection = Random.insideUnitCircle.normalized; // Set the direction
                Vector3 randomVelocity = randomDirection * 3 * Random.Range(0.1f, 1f); // Set the velocity

                Transform itemTransform = item.GetComponent<Transform>();
                itemTransform.position = transform.position + randomVelocity;
                itemTransform.rotation = Quaternion.identity;
                itemTransform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f))); // Add a random rotation to make the fragment rotate randomly
            }
        }
    }

}
