using System.Diagnostics;

namespace Judge
{
    enum Verdict { SKIP, PENDING, AC, WA, TLE, JUDGE_ERROR, RE };

    enum ErrorCode
    {
        UNDEFINED, MAJI_HAO_CHIH, WRONG_FORMAT, 
        ILLEGAL_IDX, INCORRECT, UNUSED_GROUP, 
        REPEAT_NODE, NEG_CIRCLE_DETERMINE_FAIL,
        WRONG_GRAPH_TYPE, THE_JUDGE_SKIPPED_YOUR_SUBMITION,
        HAS_ILLEGAL_CHAR, TLE, SIZE_DIFFER_FROM_N
    };

    internal class HW2_Judge
    {
        static readonly string mainLocation = Path.GetDirectoryName(AppContext.BaseDirectory)!;
        static readonly string fproc = $"{mainLocation}/proc/main";
        static readonly string fVerifyProc = $"{mainLocation}/proc/verify";
        static readonly string ioFolder = $"{mainLocation}/io";
        static readonly string fin1 = $"{ioFolder}/in1.txt";
        static readonly string fin2 = $"{ioFolder}/in2.txt";
        static readonly string fout1 = $"{ioFolder}/out1.txt";
        static readonly string fout2 = $"{ioFolder}/out2.txt";
        static readonly string fans1 = $"{ioFolder}/ans1.txt";
        static readonly string fans2 = $"{ioFolder}/ans2.txt";
        static readonly string config = $"{fin1} {fin2} {fout1} {fout2}";
        static readonly string vconfig = $"{fin1} {fin2} {fans1} {fans2}";

        public static int TestID = 1, TestSubID = 1;
        public static Verdict verdict = Verdict.PENDING;
        public static ErrorCode errorCode = ErrorCode.UNDEFINED;

        private static void WriteTest(string fin, List<List<int>> data)
        {
            using StreamWriter sw = new(fin);
            foreach (List<int> line in data)
            {
                foreach (int i in line)
                    sw.Write(i.ToString() + ' ');
                sw.WriteLine();
            }
            sw.Close();
        }
        private static List<List<int>>? ReadAns(string fout)
        {
            List<List<int>> ans = new();
            using (StreamReader sr = new(fout))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string? line = sr.ReadLine();
                        if (line == null) break;
                        line = line.TrimEnd();
                        if (fout == fout2 || fout == fans2)
                            if (line == "Negative loop detected!")
                                return null;
                        List<string> sNum = line.Split(' ').ToList();
                        List<int> nums = new();
                        foreach (string num in sNum)
                        {
                            bool legal = int.TryParse(num, out int val);
                            if (!legal)
                                throw new FormatException();
                            nums.Add(val);
                        }
                        ans.Add(nums);
                    }
                    catch (FormatException)
                    {
                        if (fout == fout1 || fout == fout2)
                            verdict = Verdict.WA;
                        else
                            verdict = Verdict.JUDGE_ERROR;
                        errorCode = ErrorCode.HAS_ILLEGAL_CHAR;
                    }
                }
                sr.Close();
            }
            return ans;
        }

        public static void Test(int ques)
        {
            Process proc = new();
            proc.StartInfo.FileName = fproc;
            proc.StartInfo.Arguments = config;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = false;

            Process vproc = new();
            vproc.StartInfo.FileName = fVerifyProc;
            vproc.StartInfo.Arguments = vconfig;
            vproc.StartInfo.UseShellExecute = false;
            vproc.StartInfo.RedirectStandardOutput = false;

            Ques1 q1;
            Ques2 q2 = new();
            DAG _DAG = new();
            Cycle _CYCLE = new();
            for (; TestID <= 60; ++TestID)
            {
                q1 = (TestID % 2 == 0) ? _DAG : _CYCLE;
                bool verifyTLE = (TestID >= 20);
                List<List<int>> data1 = q1.Gen(verifyTLE);
                List<List<int>> data2 = q2.Gen(verifyTLE);
                WriteTest(fin1, data1);
                WriteTest(fin2, data2);
                proc.Start();
                bool tle = !proc.WaitForExit(1000);
                proc.Close();
                if (tle)
                {
                    verdict = Verdict.TLE;
                    errorCode = ErrorCode.TLE;
                }
                vproc.Start();
                bool vtle = !vproc.WaitForExit(1000);
                vproc.Close();
                if (vtle)
                {
                    verdict = Verdict.TLE;
                    errorCode = ErrorCode.TLE;
                }
                List<List<int>> opt1 = ReadAns(fout1)!;
                List<List<int>> ans1 = ReadAns(fans1)!;
                List<List<int>>? opt2 = ReadAns(fout2);
                List<List<int>>? ans2 = ReadAns(fans2);
                if (verdict == Verdict.PENDING)
                {
                    if (ques != 2)
                        Ques1.Test(opt1, ans1);
                    if (ques != 1)
                        Ques2.Test(opt2, ans2);
                }

                ShowStatus();
            }

            verdict = Verdict.AC;
            ShowStatus();
        }

        public static void ShowStatus()
        {
            Console.Clear();
            if (verdict == Verdict.PENDING)
                Console.WriteLine(verdict);
            else
            {
                if (verdict == Verdict.AC)
                    Console.WriteLine(verdict);
                else
                    Console.WriteLine($"{verdict} on Test#{TestID}, Error Type: {errorCode}");
                Console.WriteLine("Press <Enter> to continue...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
