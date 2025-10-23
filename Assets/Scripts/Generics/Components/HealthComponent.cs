using UnityEngine;

namespace Generics
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        public delegate void HealthUpdateAction(GameObject gameObject = null, int value = 0);
        public static event HealthUpdateAction OnHealthUpdate;

        public delegate void DeathAction(GameObject gameObject);
        public static event DeathAction OnDeath;

        [SerializeField] protected bool m_isAlive = true;
        public bool IsAlive
            => m_isAlive;
        public bool SetIsAlive(bool value)
            => m_isAlive = value;


        [SerializeField] protected int m_currentHealth = 3;
        public virtual int CurrentHealth
            => m_currentHealth;
        public void SetCurrentHealth(int value)
            => m_currentHealth = value;


        [SerializeField] protected int m_maxHealth = 3;
        public virtual int MaxHealth
            => m_maxHealth;
        public void SetMaxHealth(int value)
        {
            m_maxHealth = value;
            ResetHealth();
        }


        protected virtual void OnEnable()
        {
            ResetHealth();
        }

        /// <summary>
        /// Reset Health to max health
        /// </summary>
        public virtual void ResetHealth()
        {
            SetIsAlive(true);
            SetCurrentHealth(MaxHealth);
        }

        public virtual void Hit(int damageValue)
        {
            if (CurrentHealth > 0)
            {
                SetCurrentHealth(CurrentHealth - damageValue);

                if (OnHealthUpdate != null)
                    OnHealthUpdate(gameObject, CurrentHealth);
            }

            if (CurrentHealth <= 0)
                Die();
        }

        public virtual void Die()
        {
            if (OnDeath != null)
                OnDeath(gameObject);

            SetIsAlive(false);
        }

    }
}
