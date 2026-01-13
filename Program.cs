namespace LiveCode
{
    // 델리게이트
    // - 메서드 참조를 지정할 수 있는 형식

    public class Program
    {
        //선언
        private delegate void LoggerDelegate(string msg);

        static void Main(string[] args)
        {
            // 2. 델리게이트 할당
            //LoggerDelegate log = Logger;
            LoggerDelegate? log = null;

            //3. 델리게이트 호출
            //log("델리게이트 호출 성공 1");

            //4. 델리게이트 체인 기법
            log += Logger;
            log += LoggerTime;
            
            log?.Invoke("델리게이트 호출 성공 : 체인 ");

            Console.WriteLine("정상 종료");

            // 5. 델리게이트 제거
            log -= Logger;
            log?.Invoke("델리게이트 호출 성공 : 체인 제거 후 ");
        }

        static void Logger(string msg)
        {
            Console.WriteLine(msg);
        }

        static void LoggerTime(string msg)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
        }
    }
}
