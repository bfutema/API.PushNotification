using System;

namespace Icomon.PushNotification.Domain.Entities.Generic
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
