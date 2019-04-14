using System;
using System.Collections.Generic;
using System.Linq;


namespace Lesson_Blockchain
{
    /// <summary>
    /// Цепочка блоков
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Все блоки
        /// </summary>
        public List<Block> Blocks { get; private set; }
        
        /// <summary>
        /// Последний добавленный блок
        /// </summary>
        public Block Last { get; private set; }

        public Chain()
        {
            Blocks = LoadChainFromDB();

            if (Blocks.Count == 0)
            {
                var genesisBlock = new Block();

                Blocks.Add(genesisBlock);
                Last = genesisBlock;
                SaveToDB(Last);
            }
            else
            {
                if (Check())
                {
                    Last = Blocks.Last();
                }
                else
                {
                    throw new Exception("Ошибка получения блоков из базы данных. Цепочка не прошла проверку.");
                }
            }                        
            
        }

        /// <summary>
        /// Добавить блок 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, Last);
            Blocks.Add(block);
            Last = block;
            SaveToDB(Last);
        }

        /// <summary>
        /// Проверка цепочки на валидность данных
        /// </summary>
        /// <returns>true - цепочка прошла проверку, false - нарушение структуры цепочки</returns>
        public bool Check()
        {
            var genesisBlock = new Block();
            var previousHash = genesisBlock.Hash;

            foreach (var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if (previousHash != hash)
                {
                    return false;
                }

                previousHash = block.Hash;
            }
            return true;
        }

        /// <summary>
        /// Метод записи блока в базу данных.
        /// </summary>
        /// <param name="block">Сохраняемый блок.</param>
        private void SaveToDB(Block block)
        {
            using (var context = new BlockchainContext())
            {
                context.Blocks.Add(block);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Получение данных из базы данных в цепочку.
        /// </summary>
        /// <returns>Список блоков данных.</returns>
        private List<Block> LoadChainFromDB()
        {
            List<Block> result;

            using (var context = new BlockchainContext())
            {
                var count = context.Blocks.Count();
                result = new List<Block>(count * 2);

                result.AddRange(context.Blocks);
            }

            return result;
        }


    }
}
