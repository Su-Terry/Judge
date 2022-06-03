using Judge;

Client.Validate();

Console.WriteLine("Which do you want to test? (0: none, 1, 2, 3: both)");
int ques = -1;
while (ques != 0 && ques != 1 && ques != 2 && ques != 3)
    ques = Console.Read() - '0';

if (ques == 0)
{
    HW2_Judge.verdict = Verdict.SKIP;
    HW2_Judge.errorCode = ErrorCode.THE_JUDGE_SKIPPED_YOUR_SUBMITION;
    HW2_Judge.ShowStatus();
}
else
    HW2_Judge.Test(ques);