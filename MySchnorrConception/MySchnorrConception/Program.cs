

namespace MySchnorrConception;


using michele.natale.Schnorrs;
using michele.natale.Schnorrs.Services;

public class Program
{
  public static void Main()
  {
    TestSchnorrGroup();
    TestSchnorrParameters();

    TestSchnorrInfo();
    TestSchnorrSignatur();

    Console.WriteLine();
    Console.WriteLine("FINISH");
    Console.ReadLine();

  }

  private static void TestSchnorrGroup()
  {
    var group = new SchnorrGroup();
    group.GenerateParameters();
    group.Reset();

    var empty = group.IsEmpty;

    group.GenerateParameters();

    var hexs = group.ToHex();
    var bytes = group.ToBytes();
    var bis = group.ToBigIntegers();

    var pmei = group.ToPmei();

    var (p, q, g, h) = SchnorrGroup.FromHex(hexs);
    var new_group_I = new SchnorrGroup((p, q, g,h));
    var new_group_II = SchnorrGroup.FromPmei(pmei);

  }


  private static void TestSchnorrParameters()
  {
    var param = new SchnorrParameters();
    param.GenerateParameters();

    param.Reset();

    var empty = param.IsEmpty;

    param.GenerateParameters(false);

    var pmei_I = param.ToPmei();
    var pmei_II = param.ToPmei(false);
    var hexs = param.ToKeysHex();
    var bytes = param.ToKeysBytes();
    var bis = param.ToKeysBigIntegers();

    var group = new SchnorrGroup();
    group.GenerateParameters();

    group = param.PQG;
    param = new SchnorrParameters(group);
    param = new SchnorrParameters(param);

    var new_param_I = new SchnorrParameters(pmei_I);
    var new_param_II = SchnorrParameters.FromPmei(pmei_II);

  }


  private static void TestSchnorrInfo()
  {
    var msg = "This is my Message!"u8.ToArray();

    var param = new SchnorrParameters();
    param.GenerateParameters();
    param.Reset();

    var empty = param.IsEmpty;

    param = new SchnorrParameters();
    param.GenerateParameters(false);
  
    var info = new SchnorrInfo(msg,param);
    var sign = info.Sign();

    var verify = info.Verify(sign);

    if (!verify) throw new Exception();

    
  }

  private static void TestSchnorrSignatur()
  {
    Console.WriteLine($"Beginn {nameof(TestSchnorrSignatur)} ...");

    var psz = SchnorrServices.P_MIN_SIZE;
    var qsz = SchnorrServices.Q_MIN_SIZE;

    var group = new SchnorrGroup();
    group.GenerateParameters();
    Console.WriteLine($"{nameof(group)} I = true");

    group = new SchnorrGroup();
    group.GenerateParameters(psz, qsz);
    Console.WriteLine($"{nameof(group)} II = true");

    var param = new SchnorrParameters();
    param.GenerateParameters(false);
    Console.WriteLine($"{nameof(param)} I = true");

    param = new SchnorrParameters();
    param.GenerateParameters(true);
    Console.WriteLine($"{nameof(param)} II = true");

    param = new SchnorrParameters();
    param.GenerateParameters(psz, qsz, true);
    Console.WriteLine($"{nameof(param)} III = true");

    param = new SchnorrParameters();
    param.GenerateParameters(psz, qsz, false);
    Console.WriteLine($"{nameof(param)} IIII = true");

    var msg = "This is my Message!"u8.ToArray();
    var schnorr_info = new SchnorrInfo(msg, param);
    Console.WriteLine($"{nameof(schnorr_info)} = true");

    var sign = schnorr_info.Sign();
    Console.WriteLine($"{nameof(sign)} = true");

    var verify = schnorr_info.Verify(sign);
    Console.WriteLine($"verifiy = {verify}");

    if (!verify) throw new Exception();
  }

}


