using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaExchangeClient.DataObjects
{
    public class Environment
    {
        /// <summary>
        /// Gets or sets the id of the environment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the environment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the name of the instance
        /// </summary>
        /// <returns>The name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
