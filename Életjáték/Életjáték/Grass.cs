using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Életjáték
{

    public enum GrassState
    {
        Seedling,  
        Tender,    
        Mature     
    }

    public class Grass
    {
        public GrassState State { get; private set; } 

        public Grass()
        {
            Random rnd = new Random();
            int StartingValue = rnd.Next(1, 4);
            switch (StartingValue)
            {
                case 1: 
                    State = GrassState.Seedling; 
                    break;
                case 2:
                    State = GrassState.Tender;
                    break;
                case 3: State = GrassState.Mature;
                    break;

            }
        }

        public void Grow()
        {
            switch (State)
            {
                case GrassState.Seedling:
                    State = GrassState.Tender;
                    break;
                case GrassState.Tender:
                    State = GrassState.Mature;
                    break;
                case GrassState.Mature:
                    State = GrassState.Tender; 
                    break;
            }
        }

        public char GetDisplayCharacter()
        {
            switch (State)
            {
                case GrassState.Seedling:
                    return '.';
                case GrassState.Tender:
                    return '+';
                case GrassState.Mature:
                    return '#';
                default:
                    return ' ';
            }
        }
    }

}
