using ClientRabbit;

namespace ClientBlazorApp
{
	public class TestClientRabbit
	{
		private readonly ConfigClientRabbit _config;
        public TestClientRabbit(ConfigClientRabbit config)
        {
			_config = config;
        }
		public void Test()
		{
			_config.Test();
		}
	}
}
