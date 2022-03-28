using System.Net;

namespace ApiProject.ApiCustomResponse
{
    public class CustomOk : ApiCustomResponse
    {
        public CustomOk() : base(200, HttpStatusCode.OK.ToString()) //Custom Response StatusCode 200
        {
        }

        public CustomOk(bool isSuccess, string message, object source) : base(200, HttpStatusCode.OK.ToString(), isSuccess, message, source)
        {
            //Custom Response StatusCode 200 : başarılı geri dönüşünü yapılandırarak ek bir isSuccess ve message
            //değişkeni ekliyorum. Böylece sadece status code üzeriden çıkarım yapmak yerine ilgili değişkenleri
            //kullanarak, api'ı consume eden kullanıcılarda daha detaylı bir alert mekanizması oluşturabiliyorum.
        }
    }
}
