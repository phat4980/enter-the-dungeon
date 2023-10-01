using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Enemy
{
    public abstract class Boss : MonoBehaviour
    {
        public float timer = 0;
        public List<GameObject> itemsToSpawn;
        [Tooltip("Auto set target")]
        public GameObject target;
        public PathFireManager pathFireManager;

        [Header("Attributes use for fire")]
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
        public PathFireBullet fire;
        public bool isFire;

        [Header("Attributes use for state")]
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
        public GameObject myHealth;
        private BossHealth healthBar;
        public bool setAlready;
        private void Awake()
        {
            pathFireManager = new PathFireManager();
            owner = gameObject;
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            stateManager = new StateManager(new Spawn(), animator);
            stateManager.EnterState();
        }
        private void Start()
        {
            myHealth = UIController.Instance.bossHealth;
            healthBar = myHealth.GetComponent<BossHealth>();

        }
        private void Update() {
            if (target != null)
            {
                if (setAlready == false)
                {
                    myHealth.SetActive(true);
                    healthBar.SetValue(HP);
                    setAlready = true;
                }
                targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
                SetAgentPos(targetPos);
                CompareTargetPositionToAgent();
                SetDir();
                if (agent.velocity != Vector3.zero && HP > 0 && isFire == false) stateManager.SwithcState(new MoveState());
                if (isFire == false && HP > 0) timer += Time.deltaTime;
                if (isFire == false && HP > 0 && timer > 3)
                {
                    isFire = true;
                    templateSkillBoss();
                }
                if (isFire == true)
                {
                    agent.isStopped = true;
                    stateManager.SwithcState(new BossAttackState());
                    timer += Time.deltaTime;
                }
                
                //if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && isFire == true && HP > 0) stateManager.SwithcState(new IdleState());
                
                if (timer > 8)
                {
                    isFire = false;
                    StopAllCoroutines();
                    timer = 0;
                    agent.isStopped = false;
                }
                
                if (HP <= 0) 
                {
                    isFire = false;
                    Die(); 
                }
            }
        }
        private void templateSkillBoss()     //template method
        {
            int randomSkill = Random.Range(0,3);
            switch(randomSkill){
                case 0:
                    SkillOne();
                    break;
                case 1:
                    SkillTwo();
                    break;
                case 2:
                    SkillThree();
                    break;
            }
        }

        private void SetAgentPos(Vector3 position)
        {
           var agentDrift = 0.0001f;
           var driftPos = position + (Vector3)(agentDrift * Random.insideUnitCircle);
           agent.SetDestination(driftPos);
        }
        private void CompareTargetPositionToAgent()
        {
            if (targetPos.x > transform.position.x + 0.5f) curHoriSide = side.R.ToString();

            if (targetPos.x < transform.position.x - 0.5f) curHoriSide = side.L.ToString();

            if (targetPos.x <= transform.position.x + 0.5f && targetPos.x >= transform.position.x - 0.5f) curHoriSide = side.O.ToString();

            if (targetPos.y > transform.position.y + 0.5f) curVerSide = side.U.ToString();
            if (targetPos.y < transform.position.y + 0.5f) curVerSide = side.D.ToString();
            if (targetPos.y <= transform.position.y + 0.5f && targetPos.y >= transform.position.y - 0.5f) curVerSide = side.O.ToString();
        }

        private void SetDir()
        {
            if (animator.parameterCount == 1)
            {
                if (animator.GetParameter(0).name == "ver") stateManager.setDirection("O", curVerSide);
                if (animator.GetParameter(0).name == "hori") stateManager.setDirection(curHoriSide, "0");
            }
            if (animator.parameterCount == 2) stateManager.setDirection(curHoriSide, curVerSide);
        }

        public void Die()
        {
            myHealth.SetActive(false);
            StopAllCoroutines();
            foreach (GameObject b in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                if (b.activeInHierarchy == true) b.GetComponent<Bullet>().Destroy();
            }
            stateManager.SwithcState(new Die());
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                //spawnAction();
                Destroy(gameObject,1.5f);
            }
        }


        public abstract void SkillOne();
        public abstract void SkillTwo();
        public abstract void SkillThree();
        public abstract void SetRandomSpecialValue();
        public void GetDmg(float dmg)
        {
            healthBar.Damage(dmg);
        }
        private void spawnAction()
        {
            int ran = Random.Range(7,15);
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
