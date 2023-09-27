using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public interface IDAO
    {
        public List<Entity> ReadAll();
        public Entity Find(int id);
        public bool Insert(Entity e);
        public bool Delete(Entity e);
        public bool Update(Entity e);
    }
}
