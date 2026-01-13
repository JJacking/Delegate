namespace LiveCode
{

    public class Program
    {

        static void Main(string[] args)
        {
            Player player = new Player("Jack",100);

            // 플레이어 사망 이벤트 발생
            //player.OnPlayerDie?.Invoke();


        }

    }

    class Player
    {
        public string Name { get; } // 읽기 전용 속성 프로퍼티
        private int _hp;            //내부 필드

        public delegate void PlayerDieHandler();
        public event PlayerDieHandler OnPlayerDie;

        public Player(string name, int hp)
        {
            Name = name;
            _hp = hp;
            OnPlayerDie += Die;
        }

        public void Die()
        {
            Console.WriteLine("플레이어가 사망했습니다.");
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            if( _hp < 0 )
            {
                OnPlayerDie?.Invoke();
            }
        }
    }
}
