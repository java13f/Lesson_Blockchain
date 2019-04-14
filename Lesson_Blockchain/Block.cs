using System;
using System.Security.Cryptography;
using System.Text;

namespace Lesson_Blockchain
{
    public class Block
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Данные
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Хэш блока
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Хэш предыдущего блока
        /// </summary>
        public string PreviousHash { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Конструктор генезис блока
        /// </summary>
        public Block()
        {
            Id = 1;
            Data = "GenesisBlock";
            Created = DateTime.Parse("11.04.2019 00:00:00.000");
            PreviousHash = "11111";
            User = "Admin";

            var data = GetData();
            Hash = GetHash(data);
        }

        /// <summary>
        /// Конструктор блока
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="block"></param>
        public Block(string data, string user, Block block)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("Пустой аргумент data", nameof(data));
            }
            if (block == null)
            {
                throw new ArgumentNullException("Пустой аргумент block", nameof(block));
            }
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException("Пустой аргумент user", nameof(user));
            }

            Data = data;
            User = user;
            PreviousHash = block.Hash;
            Created = DateTime.UtcNow;
            Id = block.Id + 1;

            var blockData = GetData();
            Hash = GetHash(blockData);
        }

        /// <summary>
        /// Получение значимых данных
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            string result = "";

            //result += Id.ToString();
            result += Data;
            result += Created.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += User;
            result += PreviousHash;

            return result;
        }

        /// <summary>
        /// Хэширование данных 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetHash(string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            var hashManaged = new SHA256Managed();
            string hex = "";

            var hashValue = hashManaged.ComputeHash(message);
            foreach (var item in hashValue)
            {
                hex += String.Format("{0:x2}", item);
            }
            return hex;
        }

        public override string ToString()
        {
            return Data +" ♣ "+User;
        }

    }
}
