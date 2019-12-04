using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    public class Monster
    {
        public enum EmotionalState
        {
            none,
            happy,
            sad,
            angry,
            bored
        }

        public enum Tribes
        {
            none,
            badChildren,
            goodChildren,
            snowballGang,
            gamers,
            glitchedMonsters
        }

        #region CONSTRUCTORS

        public Monster()
        {

        }

        public Monster(string Name, int Age, EmotionalState Attitude, Tribes Tribe, bool Active, DateTime Birthdate)
        {
            _name = Name;
            _age = Age;
            _attitude = Attitude;
            _tribe = Tribe;
            _active = Active;
            _birthdate = Birthdate;
        }

        #endregion

        #region METHODS

        public string Greeting()
        {
            string Greeting;

            if (_active)
            {
                switch (_attitude)
                {
                    case EmotionalState.none:
                        Greeting = $"Hello, my name is {_name} and I feel nothing.";
                        break;
                    case EmotionalState.happy:
                        Greeting = $"Hello, my name is {_name} and I am having a great day!";
                        break;
                    case EmotionalState.sad:
                        Greeting = $"{_name} doesn't give a greeting... {_name} is feeling sad...";
                        break;
                    case EmotionalState.angry:
                        Greeting = $"I'm {_name}. Stay away from me!";
                        break;
                    case EmotionalState.bored:
                        Greeting = $"Hey, I'm {_name}... And I'm bored...";
                        break;
                    default:
                        Greeting = $"Hello, my name is {_name}. I think I have an error.";
                        break;
                }
            }
            else
            {
                switch (_attitude)
                {
                    case EmotionalState.none:
                        Greeting = $"{_name} feels nothing.";
                        break;
                    case EmotionalState.happy:
                        Greeting = $"{_name} is having a great day!";
                        break;
                    case EmotionalState.sad:
                        Greeting = $"{_name} isn't here right now... {_name} is feeling sad...";
                        break;
                    case EmotionalState.angry:
                        Greeting = $"{_name} is angry.";
                        break;
                    case EmotionalState.bored:
                        Greeting = $"{_name} is bored...";
                        break;
                    default:
                        Greeting = $"{_name} appears to have an error.";
                        break;
                }
            }

            
            return Greeting;
        }

        public string TribeName()
        {
            string TribeName;
            switch (_tribe)
            {
                case Tribes.none:
                    TribeName = "N/A";
                    break;
                case Tribes.badChildren:
                    TribeName = "Bad Children";
                    break;
                case Tribes.goodChildren:
                    TribeName = "Good Children";
                    break;
                case Tribes.snowballGang:
                    TribeName = "Snowball Gang";
                    break;
                case Tribes.gamers:
                    TribeName = "Gamers";
                    break;
                default:
                    TribeName = "Glitched Monsters";
                    break;
            }
            return TribeName;
        }

        public Tribes TribeNameToTribe(string TribeName)
        {
            Tribes Tribe;

            switch (TribeName.ToUpper())
            {
                case "BAD CHILDREN":
                    Tribe = Tribes.badChildren;
                    break;
                case "GOOD CHILDREN":
                    Tribe = Tribes.goodChildren;
                    break;
                case "SNOWBALL GANG":
                    Tribe = Tribes.snowballGang;
                    break;
                case "GAMERS":
                    Tribe = Tribes.gamers;
                    break;
                case "NONE":
                    Tribe = Tribes.none;
                    break;
                default:
                    Tribe = Tribes.glitchedMonsters;
                    break;
            }

            return Tribe;
        }

        #endregion

        #region FIELDS

        private string _name;
        private int _age;
        private EmotionalState _attitude;
        private string _town;
        private Tribes _tribe;
        private bool _active;
        private DateTime _birthdate;



        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public EmotionalState Attitude
        {
            get { return _attitude; }
            set { _attitude = value; }
        }


        public string Town
        {
            get { return _town; }
            set { _town = value; }
        }

        public Tribes Tribe
        {
            get { return _tribe; }
            set { _tribe = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public DateTime Birthdate
        {
            get { return _birthdate; }
            set { _birthdate = value; }
        }

        #endregion




    }
}
