namespace trial
{
    public class PriorityQueue
    {
        private Queue<Enemy>[] queues;
        public void Enqueue(Enemy enemy, byte importance)
        {
            queues[importance].Enqueue(enemy);
        }
        public Enemy Dequeue()
        {
            for (int i = 0; i < queues.Length; i++)
            {
                if (queues[i].Count > 0)
                {
                    return queues[i].Dequeue();
                }
            }
            return null;
        }
        static void CheckForSummon(PriorityQueue summonQueue)
        {
            Random _rnd = new Random();
            const int limit = 100;
            if (true)
            {
                int temp = _rnd.Next(1, Convert.ToInt32(Math.Round(Math.Sqrt(limit) + 1)));
                Enemy summon = summonQueue.Dequeue();
                summon.Position.x = temp;
                summon.Position.y = Math.Sqrt(100 - Math.Pow(temp, 2));
                summon.isAlive = true;
            }
        }
        static PriorityQueue LoadEnemies()
        {
            PriorityQueue queue = new PriorityQueue();
            //add enemies
            return queue;
        }
    }
}
