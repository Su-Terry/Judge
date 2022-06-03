namespace Judge
{
    internal class Ques
    {
        public const int MIN_NODES = 2;
        public const int MAX_NODES = 1000;
        public const int MAX_EDGES = 3000;
    }

    internal abstract class Ques1 : Ques
    {
        protected Random rand = new();

        public abstract List<List<int>> Gen(bool verifyTLE);

        public static void Test(List<List<int>> output, List<List<int>> ans)
        {
            if (output.Count != ans.Count)
            {
                HW2_Judge.verdict = Verdict.WA;
                HW2_Judge.errorCode = ErrorCode.WRONG_GRAPH_TYPE;
            }
            else if (ans.Count == 1)
            {
                if (output[0].Count != ans[0].Count)
                {
                    HW2_Judge.verdict = Verdict.WA;
                    HW2_Judge.errorCode = ErrorCode.SIZE_DIFFER_FROM_N;
                }
                else
                {
                    bool[] used = new bool[ans[0].Count];
                    for (int i = 0; i < ans[0].Count; ++i)
                    {
                        if (ans[0][i] >= ans[0].Count)
                        {
                            HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                            HW2_Judge.errorCode = ErrorCode.ILLEGAL_IDX;
                            return;
                        }
                        if (used[ans[0][i]])
                        {
                            HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                            HW2_Judge.errorCode = ErrorCode.REPEAT_NODE;
                            return;
                        }
                        used[ans[0][i]] = true;
                    }
                    for (int i = 0; i < output[0].Count; ++i)
                        if (output[0][i] != ans[0][i])
                        {
                            HW2_Judge.verdict = Verdict.WA;
                            HW2_Judge.errorCode = ErrorCode.INCORRECT;
                        }
                }
            }
            else
            {
                if (ans.Count == 0 || ans[0].Count != 2)
                {
                    HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else if (output.Count == 0 || output[0].Count != 2)
                {
                    HW2_Judge.verdict = Verdict.WA;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else
                {
                    List<Tuple<int, int, int>> outE = new();
                    List<Tuple<int, int, int>> ansE = new();
                    for (int i = 1; i < ans.Count; ++i)
                    {
                        if (ans[i].Count != 3)
                        {
                            HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                            HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                            return;
                        }
                        ansE.Add(new Tuple<int, int, int>(ans[i][0], ans[i][1], ans[i][2]));
                    }
                    for (int i = 1; i < output.Count; ++i)
                    {
                        if (output[i].Count != 3)
                        {
                            HW2_Judge.verdict = Verdict.WA;
                            HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                            return;
                        }
                        outE.Add(new Tuple<int, int, int>(output[i][0], output[i][1], output[i][2]));
                    }

                    ansE.Sort();
                    outE.Sort();
                    if (ansE.Count != ans[0][1])
                    {
                        HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                        HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                    }
                    else if (outE.Count != output[0][1])
                    {
                        HW2_Judge.verdict = Verdict.WA;
                        HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                    }
                    else
                    {
                        bool[] used = new bool[ans[0][0]];
                        for (int i = 0; i < ansE.Count; ++i)
                        {
                            if (ansE[i].Item1 >= ans[0][0] || ansE[i].Item2 >= ans[0][0])
                            {
                                HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                                HW2_Judge.errorCode = ErrorCode.ILLEGAL_IDX;
                                break;
                            }
                            used[ansE[i].Item1] = used[ansE[i].Item2] = true;
                            if (ansE[i].ToString() != outE[i].ToString())
                            {
                                HW2_Judge.verdict = Verdict.WA;
                                HW2_Judge.errorCode = ErrorCode.INCORRECT;
                            }
                        }
                        if (ans[0][0] > 1)
                        {
                            for (int i = 0; i < used.Length; ++i)
                            {
                                if (!used[i])
                                {
                                    HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                                    HW2_Judge.errorCode = ErrorCode.UNUSED_GROUP;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    internal class Ques2 : Ques
    {
        private Random rand = new();
        private const int MIN_WEIGHT = -100;
        private const int MAX_WEIGHT = 10000;

        public List<List<int>> Gen(bool verifyTLE)
        {
            int nodes = (verifyTLE) ? MAX_NODES : rand.Next(MIN_NODES, MIN_NODES + 20);
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
                for (int j = 0; j < nodes; ++j)
                    if (i != j && !connect[i, j])
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
                for (int j = 0; j < nodes; ++j)
                    if (connect[i, j])
                        data.Add(new List<int> { all[i], all[j], rand.Next(MIN_WEIGHT, MAX_WEIGHT) });
            return data;
        }

        public static void Test(List<List<int>>? opt, List<List<int>>? ans)
        {
            if (opt == null && ans == null)
                return;
            else if (opt != null && ans != null)
            {
                if (ans.Count != 2)
                {
                    HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else if (opt.Count != 2)
                {
                    HW2_Judge.verdict = Verdict.WA;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else if (ans[0].Count != 1 && ans[1].Count != 1)
                {
                    HW2_Judge.verdict = Verdict.JUDGE_ERROR;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else if (opt[0].Count != 1 && opt[1].Count != 1)
                {
                    HW2_Judge.verdict = Verdict.WA;
                    HW2_Judge.errorCode = ErrorCode.WRONG_FORMAT;
                }
                else if (ans[0][0] != opt[0][0] || ans[1][0] != opt[1][0])
                {
                    HW2_Judge.verdict = Verdict.WA;
                    HW2_Judge.errorCode = ErrorCode.INCORRECT;
                }
            }
            else
            {
                HW2_Judge.verdict = Verdict.WA;
                HW2_Judge.errorCode = ErrorCode.NEG_CIRCLE_DETERMINE_FAIL;
            }
        }
    }
}
