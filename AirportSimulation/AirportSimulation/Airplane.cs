using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class Airplane
    {
        private int _id;
        private DateTime _arrivalTime; //Time plane was generated. Time the plane 'arrived' into airport system

        private String _airLine;

        private Action _action;

        public Airplane(int id, String airline, DateTime arrivalTime, Action action)
        {
            _id = id;
            _arrivalTime = arrivalTime;

            _airLine = airline;

            _action = action;
        }
        public Airplane(int id, String airline, Action action)
        {
            new Airplane(id, airline, new DateTime(), action);
        }
        public Airplane(int id, String airline)
        {
            new Airplane(id, airline, new Action());
        }
        public Airplane()
        {
            _id = 0;
            _arrivalTime = new DateTime();
            _airLine = null;
            _action = new Action();
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public DateTime ArrivalTime
        {
            get
            {
                return _arrivalTime;
            }
            set
            {
                _arrivalTime = value;
            }
        }

        public String AirLine
        {
            get
            {
                return _airLine;
            }
            set
            {
                _airLine = value;
            }
        }

        public Action Actionn
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        public enum Action
        {
            Landing = 1,
            TakeOff = 2
        }
    }
}
