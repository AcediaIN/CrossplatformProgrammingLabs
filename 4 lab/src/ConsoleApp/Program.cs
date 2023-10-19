using Common;
using JustCli;

LabExecutor.Labs.Add(Lab1.Lab.Instance);
LabExecutor.Labs.Add(Lab2.Lab.Instance);
LabExecutor.Labs.Add(Lab3.Lab.Instance);

await CommandLineParser.Default.ParseAndExecuteCommandAsync(args);
