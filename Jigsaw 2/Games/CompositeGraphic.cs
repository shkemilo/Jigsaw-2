using Jigsaw_2.Abstracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jigsaw_2.Games
{
    public class CompositeGraphic : IGraphic
    {
        protected readonly List<IGraphic> graphics;

        public CompositeGraphic()
        {
            graphics = new List<IGraphic>();
        }

        public void Add(IGraphic graphic)
        {
            graphics.Add(graphic);
        }
     
        public void AddRange(params IGraphic[] graphic)
        {
            graphics.AddRange(graphic);
        }
     
        public void Delete(IGraphic graphic)
        {
            graphics.Remove(graphic);
        }
       
        public void Print()
        {
            foreach (IGraphic childGraphic in graphics)
            {
                childGraphic.Print();
            }
        }
    }
}
