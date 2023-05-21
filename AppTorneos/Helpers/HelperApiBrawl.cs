using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace AppTorneos.Helpers
{
    public class HelperApiBrawl
    {
        string enlaceJugadores = "https://api.brawlstars.com/v1/players/%23";

        public async Task<bool> TokenApi(string idjugador)
        {
            HttpClient cliente = new HttpClient();

            cliente.DefaultRequestHeaders.Add("accept", "application/json");
            //cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjkwZDA2OTA0LTg1YTQtNDI4Yi05M2ZlLTQ1OGFiYWQ1NGJlYyIsImlhdCI6MTY4NDE3MjQ3OCwic3ViIjoiZGV2ZWxvcGVyLzE5MGJhMDMyLTlkNTYtMGNmZi0yNmI2LWRmYzU5MjljZjEyZSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiNjYuODEuMTY1LjExNyJdLCJ0eXBlIjoiY2xpZW50In1dfQ.uj2-2ySEpx4hvNaWtGJf9JNPa9tp35VWQmeyHTxpdU7vXZeJBJErfwUEaLEITedS0f4hEGs3YijXrB0X4RoUJA");
            //cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjkxZjJmNTUzLTg0YWUtNDU5OC1hM2ZlLTk5ZmUxYTNlMGUzNyIsImlhdCI6MTY4MTIxNDMwNiwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjEzLjAuNjkuMTE0Il0sInR5cGUiOiJjbGllbnQifV19.f8RFA2_l_3RbxGJvxWpXANO2Dg-aW92mN1hoWgRxJe8RJZqhrumJvPZMRrszD_af__KmhViP9jCt87G6_Gx9pg");
            cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjgwZTU0NzdjLWEwNmItNDE1YS1iZGViLWNjN2UzOGEyMzI5NSIsImlhdCI6MTY4NDIzNjIyMSwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjAuMTA1LjIzMi4yIl0sInR5cGUiOiJjbGllbnQifV19.f3btK_T6a1BgIv0ofkXDM0SXTeDwKtik5FCYu_kjcuhRUAuiB1Kcx-g19yoPiXvHpjqDY4-C1boy-JBRDBjhPg");

            //cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjQyZTg2ZWFhLWRkNzAtNDQ0ZS1hYzE1LTQwYjgxOGVkMmRiZiIsImlhdCI6MTY4NDIzNDY0MCwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjAuMTA1LjIzMi4yIl0sInR5cGUiOiJjbGllbnQifV19.OV3XIf_bTIkj_OUWMZlQwBo2UnM0OXHLsPgL5seLIaSMMtFV_VuA1WSmxpp8s6XvmSAL1duRPj9b5UQHIZ_SRQ");

            try
            {

                var playerresponse = await cliente.GetStringAsync(enlaceJugadores + idjugador);
                JObject json = JObject.Parse(playerresponse);
                json.Values().AsEnumerable().ToList();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<JObject> InfoUsuarioXTag(string tag)
        {

            JObject jsondevolver;

            HttpClient cliente = new HttpClient();

            cliente.DefaultRequestHeaders.Add("accept", "application/json");
            cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjgwZTU0NzdjLWEwNmItNDE1YS1iZGViLWNjN2UzOGEyMzI5NSIsImlhdCI6MTY4NDIzNjIyMSwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjAuMTA1LjIzMi4yIl0sInR5cGUiOiJjbGllbnQifV19.f3btK_T6a1BgIv0ofkXDM0SXTeDwKtik5FCYu_kjcuhRUAuiB1Kcx-g19yoPiXvHpjqDY4-C1boy-JBRDBjhPg");
            //cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjkxZjJmNTUzLTg0YWUtNDU5OC1hM2ZlLTk5ZmUxYTNlMGUzNyIsImlhdCI6MTY4MTIxNDMwNiwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjEzLjAuNjkuMTE0Il0sInR5cGUiOiJjbGllbnQifV19.f8RFA2_l_3RbxGJvxWpXANO2Dg-aW92mN1hoWgRxJe8RJZqhrumJvPZMRrszD_af__KmhViP9jCt87G6_Gx9pg");
            //cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjQyZTg2ZWFhLWRkNzAtNDQ0ZS1hYzE1LTQwYjgxOGVkMmRiZiIsImlhdCI6MTY4NDIzNDY0MCwic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiMjAuMTA1LjIzMi4yIl0sInR5cGUiOiJjbGllbnQifV19.OV3XIf_bTIkj_OUWMZlQwBo2UnM0OXHLsPgL5seLIaSMMtFV_VuA1WSmxpp8s6XvmSAL1duRPj9b5UQHIZ_SRQ");

            string enlaceSolicitud = $"{enlaceJugadores}{tag}";

            try
            {

                var playerresponse = await cliente.GetStringAsync(enlaceSolicitud);
                jsondevolver = JObject.Parse(playerresponse);

            }
            catch (Exception ex)
            {
                jsondevolver = new JObject();
            }

            return jsondevolver;
        }
    }
}
