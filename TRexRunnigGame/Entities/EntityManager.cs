
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRexRunnigGame.Entities
{
    public class EntityManager
    {

        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();

        public IEnumerable<IGameEntity> Entities => new ReadOnlyCollection<IGameEntity>(_entities);
    public void Update (GameTime gametime)
        {
    
            EntityManager em  = new EntityManager();

           

            foreach (IGameEntity entity in _entities)
            {
                entity.Update(gametime);

            }

            foreach(IGameEntity entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }

            foreach (IGameEntity entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        } 

        public void Drew(SpriteBatch spritebatch, GameTime gametime)
        {

            foreach (IGameEntity entity in _entities.OrderBy(_entities => _entities.DrewOrder))
            {

                entity.Drew(spritebatch, gametime);

            }

        }

        public void AddEntity(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof (entity), "null cannot be added as an entity");

            _entitiesToAdd.Add(entity);
        }

        public void RemoveEntity( IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "null cannot be added as an entity");

            _entitiesToRemove.Add(entity);
        }

        public void Clear()
        {
            _entitiesToRemove.AddRange(_entities);

        }
    }
}
