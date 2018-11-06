using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class Runway
    {
        private Queue<Airplane> _landing;
        private Queue<Airplane> _takeOff;
        private readonly int MAX_ITEMS = 4; //Max number of planes in either queue
        private readonly int TIME_STEPS = 5; //How many minutes each step should simulate

        private DateTime _currentTime;
        private int _landWaitTime;
        private int _takeOffWaitTime;

        private int _landingId;
        private int _takeOffId;

        private int _nPlanes; //From spec: to store number of planes processed
        private int _nRefuse; //From spec: to store number of planes refused to land on airport
        private int _nTakeoff;//From spec: to store number of planes taken off
        private int _nLand; //From spec: to store number of planes landed

        private Random _ran;

        private static String[] _AIRPORTS = new String[] { "LAX", "DCA", "BWI", "JFK", "SEA", "SJC", "SFO", "PHX" };
        private static String[] _AIRLINES = new String[] { "Delta", "Southwest", "American", "Alaska", "Spirit", "Jet Blue" };

        public Runway()
        {
            _landing = new Queue<Airplane>();
            _takeOff = new Queue<Airplane>();

            DateTime now = DateTime.Today;
            _currentTime = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0); // Start at 12PM 'today'
            _landWaitTime = 0;
            _takeOffWaitTime = 0;

            _landingId = 0; //Odd integers for landing
            _takeOffId = 0; //Even integers for take off

            _nPlanes = 0;
            _nRefuse = 0;
            _nTakeoff = 0;
            _nLand = 0;

            _ran = new Random();
        }

        public void RunSimulation(int runways)
        {
            DateTime totalTime = _currentTime.AddMinutes(120); //Simulate 120 minutes of activity.

            while(_currentTime <= totalTime)
            {
                Console.WriteLine("The time is: {0}", _currentTime.ToString());
                int numLanding = _ran.Next(0, 4); //Generate numbers 0-3. For the intial setup of the runways
                int numTakeOff = _ran.Next(0, 4);

                GeneratePlane(Airplane.Action.Landing, numLanding);
                GeneratePlane(Airplane.Action.TakeOff, numTakeOff);
                
                _landWaitTime += _landing.Count * TIME_STEPS;
                _takeOffWaitTime += _takeOff.Count * TIME_STEPS;

                Console.WriteLine("{0} planes requesting to land", numLanding);
                Console.WriteLine("{0} planes reuqesting to take off", numTakeOff);

                RunStep(runways);

                _currentTime = _currentTime.AddMinutes(TIME_STEPS);

            }
            Console.WriteLine("FINAL STATISTICS:");
            Console.WriteLine("Landing Queue:");
            WriteQueueContents(_landing);
            Console.WriteLine("Take Off Queue:");
            WriteQueueContents(_takeOff);
            Console.WriteLine("Landings Completed: {0}", _nLand);
            Console.WriteLine("Take Offs Completed: {0}", _nTakeoff);
            Console.WriteLine("Planes Refused: {0}", _nRefuse);
            Console.WriteLine("Average Landing Wait Time: {0}", _landWaitTime / _nLand);
            Console.WriteLine("Average Take Off Wait Time: {0}", _takeOffWaitTime / _nTakeoff);

            

        }

        private void RunStep(int runways)
        {
            //From spec:  priority is given to planes requesting a landing.
            //Landing queue should be taken care of before considering takeoff
            for(int currentRunway = 0; currentRunway < runways; currentRunway++) //Scale the program to any number of runways
            {
                Airplane tmp;
                if(_landing.Count != 0 && _landing.Count >= _takeOff.Count)
                {
                    _nLand++;
                    tmp = _landing.Dequeue();
                    Console.WriteLine("{0}'s (ID: {1}) plane just landed on runway {2}", tmp.AirLine, tmp.Id, currentRunway + 1);
                }
                else if(_takeOff.Count != 0) //Check to make sure there is actually data to dequeue. In some simulations there may be a queue with 0 elements in it
                {
                    _nTakeoff++;
                    tmp = _takeOff.Dequeue();
                    Console.WriteLine("{0}'s (ID: {1}) plane just took-off from runway {2}", tmp.AirLine, tmp.Id, currentRunway + 1);
                }
            }
            Console.WriteLine();
        }
        private void GeneratePlane(Airplane.Action act, int howMany)
        {
            Airplane tmp;
            for (int i = 0; i < howMany; i++)
            {
                if (act == Airplane.Action.Landing)
                {
                    _landingId = (_nPlanes * 2) + 1; //Odd numbers: (2 * i) + 1
                    tmp = new Airplane(_landingId, GenerateAirline(), _currentTime, act);
                }
                else
                {
                    _takeOffId = ( _nPlanes * 2); //Even numbers: (2 * i)
                    tmp = new Airplane(_takeOffId, GenerateAirline(), _currentTime, act);
                }

                bool valid = ValidAction(tmp);
                if (valid && tmp.Actionn == Airplane.Action.Landing)
                {
                    _landing.Enqueue(tmp);
                }
                else if (valid && tmp.Actionn == Airplane.Action.TakeOff)
                {
                    _takeOff.Enqueue(tmp);
                }

                _nPlanes++;
            }
            return;
        }
        private bool ValidAction(Airplane plane)
        {
            if(plane.Actionn == Airplane.Action.Landing && _landing.Count >= MAX_ITEMS)
            {
                _nRefuse++;
                Console.WriteLine("PlaneID: {0} is not cleared to land. Reason: Too many planes in queue to land", plane.Id);
                return false; //Action is not valid because queue is full
            }
            else if(plane.Actionn == Airplane.Action.TakeOff && _takeOff.Count >= MAX_ITEMS)
            {
                _nRefuse++;
                Console.WriteLine("PlaneID: {0} is not cleared to take off. Reason: Too many planes in queue to take off", plane.Id);
                return false; //Action is not valid because queue is full
            }
            else return true; //Queues are not full, action is valid
        }

        private String GenerateAirline()
        {
            return _AIRLINES[_ran.Next(_AIRLINES.Length - 1)];
        }
        private void WriteQueueContents(Queue<Airplane> queue)
        {
            foreach(Airplane item in queue)
            {
                Console.WriteLine("\t{0}, #{1}, {2} ", item.AirLine, item.Id, item.ArrivalTime);
            }
        }
    }
}
