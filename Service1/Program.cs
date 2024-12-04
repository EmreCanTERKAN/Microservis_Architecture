using Grpc.Net.Client;
using Service2;

//var channel = GrpcChannel.ForAddress("http://localhost:5008");

//var greetClient = new Greeter.GreeterClient(channel);

//var response = greetClient.SayHello(new HelloRequest { Name = Console.ReadLine() });
//await Task.Run(async () =>
//{
//    while (await response.ResponseStream.MoveNext(new CancellationToken())) ;
//    Console.WriteLine($"Gelen Mesaj: {response.ResponseStream.Current.Message}");
//});

var channel = GrpcChannel.ForAddress("http://localhost:5008");
var greetClient = new Greeter.GreeterClient(channel);

var response = greetClient.SayHello(new HelloRequest { Name = Console.ReadLine() });

while (await response.ResponseStream.MoveNext(CancellationToken.None))
{
    var message = response.ResponseStream.Current;
    Console.WriteLine($"Gelen Mesaj: {message.Message}");
}

Console.Read();