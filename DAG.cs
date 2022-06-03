namespace Judge
{
    internal class DAG : Ques1
    {
        public override List<List<int>> Gen(bool verifyTLE)
        {
            int nodes = (verifyTLE) ? MAX_NODES : rand.Next(MIN_NODES, MIN_NODES + 10);
            bool[,] connect = new bool[nodes, nodes];
            int edgeCnt = 0;
            for (int i = 0; i < nodes - 1; ++i)
            {
                int next = rand.Next(i + 1, nodes - 1);
                connect[i, next] = true;
                edgeCnt++;
            }
            for (int j = 1; j < nodes; ++j)
            {
                int prev = rand.Next(0, j - 1);
                if (!connect[prev, j])
                {
                    connect[prev, j] = true;
                    edgeCnt++;
                }
            }
            int bonus = rand.Next(edgeCnt, MAX_EDGES) - edgeCnt;
            List<Tuple<int, int>> unUsed = new();
            for (int i = 0; i < nodes; ++i)
                for (int j = i + 1; j < nodes; ++j)
                    if (!connect[i, j])
                        unUsed.Add(new Tuple<int, int>(i, j));

            for (int i = unUsed.Count - 1; i >= Math.Max(0, unUsed.Count - 1 - bonus); --i)
            {
                int randid = rand.Next(i + 1);
                while (connect[unUsed[randid].Item1, unUsed[randid].Item2])
                    randid = rand.Next(i + 1);
                (unUsed[randid], unUsed[i]) = (unUsed[i], unUsed[randid]);
                connect[unUsed[i].Item1, unUsed[i].Item2] = true;
                ++edgeCnt;
            }

            int[] all = new int[nodes];
            for (int i = 0; i < nodes; ++i)
                all[i] = i;

            for (int i = nodes - 1; i >= 0; --i)
            {
                int randid = rand.Next(i + 1);
                (all[randid], all[i]) = (all[i], all[randid]);
            }

            List<List<int>> data = new();
            data.Add(new List<int> { nodes, edgeCnt });
            for (int i = 0; i < nodes; ++i)
                for (int j = i + 1; j < nodes; ++j)
                    if (connect[i, j])
                        data.Add(new List<int> { all[i], all[j], 1 });

            return data;
        }
    }
}
