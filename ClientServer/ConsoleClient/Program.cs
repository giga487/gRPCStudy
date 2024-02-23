using CharStatus;
using CommunicationLayer.Login;
using CommunicationLayer.Services;
using Core.LifeThings;
using Core.Mobile;
using Core.Space;
using Grpc.Net.Client;


namespace ConsoleClient
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            double X = 10;
            double Y = 10;
            double Z = 0;


            ILocalizable pippo = new Player()
            {
                Name = "Pippo",
                Pose = new Core.Space.Pose()
                {
                    Position = new Position()
                    {
                        X = X,
                        Y = Y,
                        Z = Z
                    }
                }
            };

            double deltaX = 10;



        }
    }
}
