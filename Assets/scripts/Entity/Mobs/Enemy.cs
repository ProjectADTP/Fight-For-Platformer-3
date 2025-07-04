using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(Vision))]
[RequireComponent(typeof(EntityDamage))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(EntityHealth))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vulnerability _vulnerability;

    private Patrol _patrol;
    private EnemyMover _mover;
    private Vision _vision;
    private EntityDamage _damage;
    private EntityHealth _health;

    private void Awake()
    {
        _damage = GetComponent<EntityDamage>();
        _patrol = GetComponent<Patrol>();
        _mover = GetComponent<EnemyMover>();
        _vision = GetComponent<Vision>();
        _health = GetComponent<EntityHealth>();
    }

    private void OnEnable()
    {
        _vision.SeeTheTarget += GoToTarget;
        _vision.LostTheTarget += StartPatrol;

        _vulnerability.TakedDamage += Destruction;

        _health.ChangedHealth += TakeDamage;
    }

    private void OnDestroy()
    {
        _vision.LostTheTarget -= StartPatrol;
        _vision.SeeTheTarget -= GoToTarget;

        _vulnerability.TakedDamage -= Destruction;

        _health.ChangedHealth -= TakeDamage;
    }

    private void Start()
    {
        StartPatrol();
    }

    private void StartPatrol()
    {
        _mover.GoToPoints(_patrol.GivePoints());
    }

    private void GoToTarget(Player target)
    {
        _mover.Initialise(target);
    }

    private void TakeDamage(int damage)
    {
        if (_health.Value <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int GetDamage()
    {
        return _damage.Damage;
    }
}