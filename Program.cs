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
            player.TakeDamage(40);

            // 이벤트 연결 해지 구독 해지
            player.OnHpChanged -= DisplayHp;
        }

        static void DisplayHp(int hp)
        {
            Console.WriteLine($"플레이어 체력이 변경됨 : {hp}");
        }

    }

    class Player
    {
        public string Name { get; } // 읽기 전용 속성 프로퍼티
        private int _hp;            //내부 필드

        //이벤트를 위한 델리게이트 정의
        public delegate void PlayerDieHandler();
        //이벤트 선언
        public event PlayerDieHandler OnPlayerDie;

        // 이벤트를 정의 및 선언
        // Action
        // - .NET 에서 제공하는 내장 델리게이트
        public event Action<int> OnHpChanged;


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
    }
}
