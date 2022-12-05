namespace DelegateAndEvent
{
    internal class CarEvent : LessonOnDelegate.Car
    {
        // This delegate works in conjunction with the
        // Car's events.
        public delegate void CarEngineHandler(string msgForCaller);
        // This car can send these events.
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;
        private bool _carIsDead;

        public void Accelerating(int delta)
        {
            _carIsDead = ReturnCarIsDead();
            // If the car is dead, fire Exploded event.
            if (_carIsDead)
            {
                Exploded?.Invoke("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;
                // Almost dead?
                if (10 == MaxSpeed - CurrentSpeed)
                {
                    AboutToBlow?.Invoke("Careful buddy! Gonna blow!");
                }
                // Still OK!
                if (CurrentSpeed >= MaxSpeed)
                {
                    _carIsDead = true;
                }
                else
                {
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                }
            }
        }
    }
}
