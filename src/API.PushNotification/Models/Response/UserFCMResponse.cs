using System;
using Newtonsoft.Json;

namespace API.PushNotification.Models.Response
{
    public class UserFCMResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("idApp")]
        public int IdApp { get; set; }

        [JsonProperty("re")]
        public string Re { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [JsonProperty("dataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [JsonIgnore]
        [JsonProperty("dataExclusao")]
        public DateTime? DataExclusao { get; set; }
    }
}