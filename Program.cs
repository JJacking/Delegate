namespace LiveCode
{

    public class Program
    {

        static void Main(string[] args)
        {
            Player player = new Player("Jack",100);

            // 다른 클레스에서 발생하는 이벤트를 구독 (Subscribe)
            //익명 메서드 / 무명 델리게이트  : 이름이 없어도 바로 1회성으로 생성 , 사용
            //player.OnHpChanged += delegate (int hp)
            //{
            //    Console.WriteLine($"플레이어 체력이 변경됨 : {hp}");
            //};

            // 람다식 
            // 이름 없는 아주 짧은 메서드 : 한번 사용하고 버리는 메서드
            // 문법 : (매개변수) => {실행문;}
            //player.OnHpChanged += (hp) => Console.WriteLine($"플레이어 체력이 변경됨 : {hp}");

            // 옵저버 패턴 
            player.OnHpChanged += DisplayHp;

            player.TakeDamage(20);
            player.TakeDamage(40);

            player.Heal(30);

            player.TakeDamage(40);

            // 이벤트 연결 해지 구독 해지 (메모리 누수를 막기위해 필수 __ 잊지말것)
            player.OnHpChanged -= DisplayHp;
        }

        static void DisplayHp(int hp)
        {
            Console.WriteLine($"플레이어 체력이 변경됨 : {hp}");
        }

    }

    class Player : IHealable
    {
        public string Name { get; } // 읽기 전용 속성 프로퍼티
        private int _hp;            //내부 필드

        //이벤트를 위한 델리게이트 정의
        public delegate void PlayerDieHandler();
        //이벤트 선언
        public event PlayerDieHandler OnPlayerDie;

        // 이벤트를 정의 및 선언
        // Action 델리게이트
        // - .NET 에서 제공하는 내장 델리게이트
        // Action<T> : 매개변수는 T형식, 반환형은 void
        // Action<T1,T2,T3...T16> : 16개 까지 가능
        public event Action<int> OnHpChanged;

        //Func 델리게이트 (반환형이 있는 경우)
        // Func<T1, T2, TResult> : 매개변수는 T1, T2, 형식 반환형은 TResult

        // (현재 HP, 회복량 ) => 회복 후 HP
        public event Func<int, int, int> OnHealing = (currHp, healAmount) => currHp + healAmount;


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
            OnHpChanged?.Invoke(_hp); // 이벤트 발생
            //Console.WriteLine($"Damage : {damage}, HP:{_hp}");
            if( _hp <= 0 )
            {
                OnPlayerDie?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            _hp = OnHealing(_hp, amount);
            Console.WriteLine($"HP 회복됨 : {_hp} [회복량 : {amount}]");
        }
    }

    // 힐러 인터페이스
    interface IHealable
    {
        void Heal(int amount);
    }
}
