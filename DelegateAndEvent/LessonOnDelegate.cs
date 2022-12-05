namespace DelegateAndEvent
{
    internal class LessonOnDelegate
    {
        // This class contains methods BinaryOp will
        // point to.

        // SimpleDelegate;
        public class SimpleMath
        {
            public int Add(int x, int y) => x + y;
            public int Subtract(int x, int y) => x - y;

        }
       public static void DisplayDelegateInfo(Delegate delObj)
        {
            // Print the names of each member in the
            // delegate's invocation list.
            foreach (Delegate del in delObj.GetInvocationList())
            {
                Console.WriteLine("Method Name: {0}", del.Method);
                Console.WriteLine("Type Name: {0}", del.Target);
            }
        }

        public class Car
        {
            // Internal state data.
            public int CurrentSpeed { get; set; }
            public int MaxSpeed { get; set; } = 100;
            public string PetName { get; set; }
            // Is the car alive or dead?
            private bool _carIsDead;
            // Class constructors.
            public Car() { }
            public Car(string name, int maxSpeed, int currentSpeed)
            {
                CurrentSpeed = currentSpeed;
                MaxSpeed = maxSpeed;
                PetName = name;
            }

            // 1) Define a delegate type.
            public delegate void CarEngineHandler(string msgForCaller);
            // 2) Define a member variable of this delegate.
            private CarEngineHandler _listOfHandlers;
            // 3) Add registration function for the caller.
            public void RegisterWithCarEngine(CarEngineHandler methodToCall)
            {
                _listOfHandlers = methodToCall;
            }

            //method tha will return _carIsDead outside this class
            public bool ReturnCarIsDead()
            {
                return _carIsDead;
            }
            // 4) Implement the Accelerate() method to invoke the delegate's
            // invocation list under the correct circumstances.
            public void Accelerate(int delta)
            {
                // If this car is "dead," send dead message.
                if (_carIsDead)
                {
                    _listOfHandlers?.Invoke("Sorry, this car is dead...");
                }
                else
                {
                }
                CurrentSpeed += delta;
                // Is this car "almost dead"?
                if (10 == (MaxSpeed - CurrentSpeed))
                {
                    _listOfHandlers?.Invoke("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                {
                    _carIsDead = true;
                }
                else
                {
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                }
            }

            /// <summary>
            /// Enabling Multicasting
            ///Recall that.NET delegates have the built-in ability to multicast.In other words, a delegate object can
            ///maintain a list of methods to call, rather than just a single method. When you want to add multiple methods
            ///to a delegate object, you simply use the overloaded += operator, rather than a direct assignment.To enable
            ///multicasting on the Car class, you could update the RegisterWithCarEngine() method, like so:
            /// </summary>


            // Now with multicasting support!
            // Note we are now using the += operator, not
            // the assignment operator (=).
            public void RegisterWithCarEngineMultiCasting(CarEngineHandler methodToCall)
                {
                    _listOfHandlers += methodToCall;
                }

            /// <summary>
            ///Code illustration on the implication of using public delegate members
            /// </summary>
            /// <param name="msgForCaller"></param>

            public delegate void CarEngineHandlerTwo(string msgForCaller);
            // Now a public member!
            public CarEngineHandlerTwo ListOfHandlers;
            // Just fire out the Exploded notification.
            public void Accelerated()
            {
                ListOfHandlers?.Invoke("Sorry, this car is dead...");
            }
        }
    }
}
