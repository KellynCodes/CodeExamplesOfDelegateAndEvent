using static DelegateAndEvent.LessonOnDelegate;

namespace DelegateAndEvent
{
    internal class Program
    {
        /// <summary>
        /// first delegate example
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int x+y</returns>
        public delegate int BinaryOp(int x, int y);
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Delegate Example *****\n");
            ///<summary>
            ///Create a BinaryOp delegate object that
            /// "points to" SimpleMath.Add().
            ///</summary>
            SimpleMath simpleMath = new();
            BinaryOp SimpleAddDelegate = new(simpleMath.Add);
            SimpleAddDelegate += simpleMath.Add;
            ///<summary>
            /// Invoke Add() method indirectly using delegate object.
            ///</summary>
            var returnValue = SimpleAddDelegate.Invoke(10, 10);
            var returnValueWithoutInvoke = SimpleAddDelegate(10, 10);
            Console.WriteLine("10 + 10 is {0}", returnValue);
            Console.WriteLine("10 + 10 is {0}", returnValueWithoutInvoke);
            ///<summary>
            /// Additional type definitions must be placed at the end of the
            /// top-level statements
            /// This delegate can point to any method,
            /// taking two integers and returning an integer.
            /// </summary>

            //=================================Investigating a delegate object==========================
            DisplayDelegateInfo(SimpleAddDelegate);

            ///<summary>
            ///Car part lesson
            /// </summary>

            Console.WriteLine("** Delegates as event enablers **\n");
            // First, make a Car object.
            Car car = new("SlugBug", 100, 10);
            // Now, tell the car which method to call
            // when it wants to send us messages.
            car.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            // Speed up (this will trigger the events).
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
            }
            // This is the target for incoming events.
            static void OnCarEngineEvent(string msg)
            {
                Console.WriteLine("\n*** Message From Car Object ***");
                Console.WriteLine("=> {0}", msg);
                Console.WriteLine("********************\n");
            }

            //Using public delegates
            Console.WriteLine("***** Agh! No Encapsulation! *****\n");
            // Make a Car.
            CarEvent myCar = new();
            // We have direct access to the delegate!
            myCar.ListOfHandlers = CallWhenExploded;
            myCar.IsAccelerating(10);
            // We can now assign to a whole new object...
            // confusing at best.
            myCar.ListOfHandlers = CallHereToo;
            myCar.IsAccelerating(10);
            // The caller can also directly invoke the delegate!
            myCar.ListOfHandlers.Invoke("hee, hee, hee...");
            static void CallWhenExploded(string msg)
            {
                Console.WriteLine(msg);
            }
            static void CallHereToo(string msg)
            {
                Console.WriteLine(msg);

            }
        }
    }
}