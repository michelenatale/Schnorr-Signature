


namespace MyECSchnorrConceptionTest;

using michele.natale.Schnorrs;
using static michele.natale.Schnorrs.EcServices.EcSchnorrServices;

public class Program
{
  public static void Main()
  {
    TestEcSchnorrCurve();

    TestEcSchnorr();
  }

  private static void TestEcSchnorrCurve()
  {
    //Random Curve
    var curve = RngEcCurve();

    //Generate all EcCurveParameters 
    var param = RngEcCurveParameters();

  }

  public static void TestEcSchnorr()
  {
    //Random Curve
    var curve = RngEcCurve();

    //New Instance of EcSchnorrParameters 
    var ec_schnorr_param = new EcSchnorrParameters(curve);

    //Generate all Parameters into EcSchnorrParameters.
    //All EcCurveParameters is included.
    //without_keypair = false. 
    ec_schnorr_param.GenerateParameters(false);

    //A Message 
    var message = "This is my Message!"u8.ToArray();

    //Generate the signature.
    var signature = Sign(message, ec_schnorr_param);

    //Check Verify.
    var verify = Verify(message, signature, ec_schnorr_param);

    //Output and check from result.
    Console.WriteLine(verify);
    if (!verify) throw new Exception();
  }
}
