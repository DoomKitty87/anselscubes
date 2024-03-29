using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    private struct PosData
    {

      public Vector3 position;
      public Quaternion rotation;
    }

    [SerializeField] private GameObject[] cubies;    
    [SerializeField] private float moveSpeed = 1;

    private int[] cubieData = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
    private List<string> moveBuffer = new List<string>();
    private PosData[] cubieInit = new PosData[26];
    private bool turning = false;
    void Start()
    {
      int i = 0;
      foreach (GameObject obj in cubies) {
        cubieInit[i].position = obj.transform.position;
        cubieInit[i].rotation = obj.transform.rotation;
        i++;
      }
    }

    void Update()
    {
      if (!turning && moveBuffer.Count != 0) ExecuteNextTurn();
      if (Input.GetKeyDown(KeyCode.R)) AddToBuffer("R");
      else if (Input.GetKeyDown(KeyCode.F)) AddToBuffer("RPrime");
      else if (Input.GetKeyDown(KeyCode.Q)) AddToBuffer("L");
      else if (Input.GetKeyDown(KeyCode.A)) AddToBuffer("LPrime");
      else if (Input.GetKeyDown(KeyCode.W)) AddToBuffer("U");
      else if (Input.GetKeyDown(KeyCode.E)) AddToBuffer("UPrime");
      else if (Input.GetKeyDown(KeyCode.D)) AddToBuffer("D");
      else if (Input.GetKeyDown(KeyCode.S)) AddToBuffer("DPrime");
      else if (Input.GetKeyDown(KeyCode.V)) AddToBuffer("F");
      else if (Input.GetKeyDown(KeyCode.C)) AddToBuffer("FPrime");
      else if (Input.GetKeyDown(KeyCode.Z)) AddToBuffer("B");
      else if (Input.GetKeyDown(KeyCode.X)) AddToBuffer("BPrime");
      else if (Input.GetKeyDown(KeyCode.P)) AddToBuffer("M");
      else if (Input.GetKeyDown(KeyCode.O)) AddToBuffer("MPrime");
      else if (Input.GetKeyDown(KeyCode.Semicolon)) AddToBuffer("E");
      else if (Input.GetKeyDown(KeyCode.L)) AddToBuffer("EPrime");
      else if (Input.GetKeyDown(KeyCode.Slash)) AddToBuffer("S");
      else if (Input.GetKeyDown(KeyCode.Period)) AddToBuffer("SPrime");
      else if (Input.GetKeyDown(KeyCode.I)) AddToBuffer("RWide");
      else if (Input.GetKeyDown(KeyCode.K)) AddToBuffer("RWidePrime");
      else if (Input.GetKeyDown(KeyCode.T)) AddToBuffer("LWide");
      else if (Input.GetKeyDown(KeyCode.G)) AddToBuffer("LWidePrime");
      else if (Input.GetKeyDown(KeyCode.Y)) AddToBuffer("UWide");
      else if (Input.GetKeyDown(KeyCode.U)) AddToBuffer("UWidePrime");
      else if (Input.GetKeyDown(KeyCode.H)) AddToBuffer("DWide");
      else if (Input.GetKeyDown(KeyCode.J)) AddToBuffer("DWidePrime");
      else if (Input.GetKeyDown(KeyCode.N)) AddToBuffer("FWide");
      else if (Input.GetKeyDown(KeyCode.B)) AddToBuffer("FWidePrime");
      else if (Input.GetKeyDown(KeyCode.Comma)) AddToBuffer("BWide");
      else if (Input.GetKeyDown(KeyCode.M)) AddToBuffer("BWidePrime");
      else if (Input.GetKeyDown(KeyCode.RightBracket)) {
        AddToBuffer("R");
        AddToBuffer("U");
        AddToBuffer("RPrime");
        AddToBuffer("UPrime");
      }
      else if (Input.GetKeyDown(KeyCode.LeftBracket)) {
        AddToBuffer("L");
        AddToBuffer("UPrime");
        AddToBuffer("LPrime");
        AddToBuffer("U");
      }
      else if (Input.GetKeyDown(KeyCode.Tab)) ResetCube();
    }

    private void ResetCube() {
      moveBuffer = new List<string>();
      StopCoroutine("R");
      StopCoroutine("RPrime");
      StopCoroutine("L");
      StopCoroutine("LPrime");
      StopCoroutine("U");
      StopCoroutine("UPrime");
      StopCoroutine("D");
      StopCoroutine("DPrime");
      StopCoroutine("F");
      StopCoroutine("FPrime");
      StopCoroutine("B");
      StopCoroutine("BPrime");
      StopCoroutine("M");
      StopCoroutine("MPrime");
      StopCoroutine("E");
      StopCoroutine("EPrime");
      StopCoroutine("S");
      StopCoroutine("SPrime");
      StopCoroutine("RWide");
      StopCoroutine("RWidePrime");
      StopCoroutine("LWide");
      StopCoroutine("LWidePrime");
      StopCoroutine("UWide");
      StopCoroutine("UWidePrime");
      StopCoroutine("DWide");
      StopCoroutine("DWidePrime");
      StopCoroutine("FWide");
      StopCoroutine("FWidePrime");
      StopCoroutine("BWide");
      StopCoroutine("BWidePrime");
      int i = 0;
      foreach (GameObject obj in cubies) {
        obj.transform.position = cubieInit[i].position;
        obj.transform.rotation = cubieInit[i].rotation;
        i++;
      }
      cubieData = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
    }

    private void ExecuteNextTurn() {
      StartCoroutine(moveBuffer[0]);
      moveBuffer.Remove(moveBuffer[0]);
      moveBuffer.TrimExcess();
    }

    private void AddToBuffer(string move) {
      moveBuffer.Add(move);
    }

    private IEnumerator R() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[8], cubieData[3], cubieData[4], cubieData[16], cubieData[6], cubieData[7], cubieData[25], cubieData[9], cubieData[10], cubieData[5], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[22], cubieData[17], cubieData[18], cubieData[2], cubieData[20], cubieData[21], cubieData[11], cubieData[23], cubieData[24], cubieData[19]};
      turning = false;
    }

    private IEnumerator RPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[19], cubieData[3], cubieData[4], cubieData[11], cubieData[6], cubieData[7], cubieData[2], cubieData[9], cubieData[10], cubieData[22], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[5], cubieData[17], cubieData[18], cubieData[25], cubieData[20], cubieData[21], cubieData[16], cubieData[23], cubieData[24], cubieData[8]};
      turning = false;
    }

    private IEnumerator L() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[12].position, Vector3.forward, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[6], cubieData[1], cubieData[2], cubieData[14], cubieData[4], cubieData[5], cubieData[23], cubieData[7], cubieData[8], cubieData[3], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[20], cubieData[15], cubieData[16], cubieData[0], cubieData[18], cubieData[19], cubieData[9], cubieData[21], cubieData[22], cubieData[17], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator LPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[12].position, Vector3.forward, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[17], cubieData[1], cubieData[2], cubieData[9], cubieData[4], cubieData[5], cubieData[0], cubieData[7], cubieData[8], cubieData[20], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[3], cubieData[15], cubieData[16], cubieData[23], cubieData[18], cubieData[19], cubieData[14], cubieData[21], cubieData[22], cubieData[6], cubieData[24], cubieData[25]};
      turning = false;
    }
    
    private IEnumerator U() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[21].position, Vector3.up, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[19], cubieData[22], cubieData[25], cubieData[18], cubieData[21], cubieData[24], cubieData[17], cubieData[20], cubieData[23]};
      turning = false;
    }

    private IEnumerator UPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[21].position, Vector3.up, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[23], cubieData[20], cubieData[17], cubieData[24], cubieData[21], cubieData[18], cubieData[25], cubieData[22], cubieData[19]};
      turning = false;
    }

    private IEnumerator D() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[6], cubieData[3], cubieData[0], cubieData[7], cubieData[4], cubieData[1], cubieData[8], cubieData[5], cubieData[2], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator DPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[2], cubieData[5], cubieData[8], cubieData[1], cubieData[4], cubieData[7], cubieData[0], cubieData[3], cubieData[6], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator F() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[2], cubieData[11], cubieData[19], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[1], cubieData[10], cubieData[18], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[0], cubieData[9], cubieData[17], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator FPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[17], cubieData[9], cubieData[0], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[18], cubieData[10], cubieData[1], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[19], cubieData[11], cubieData[2], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator B() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[15].position, Vector3.right, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[23], cubieData[14], cubieData[6], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[24], cubieData[15], cubieData[7], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[25], cubieData[16], cubieData[8]};
      turning = false;
    }

    private IEnumerator BPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
            obj.transform.RotateAround(cubieInit[15].position, Vector3.right, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
          }
          rot += moveSpeed;
          yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[8], cubieData[16], cubieData[25], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[7], cubieData[15], cubieData[24], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[6], cubieData[14], cubieData[23]};
      turning = false;
    }

    private IEnumerator M() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[18], cubieData[2], cubieData[3], cubieData[10], cubieData[5], cubieData[6], cubieData[1], cubieData[8], cubieData[9], cubieData[21], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[4], cubieData[16], cubieData[17], cubieData[24], cubieData[19], cubieData[20], cubieData[15], cubieData[22], cubieData[23], cubieData[7], cubieData[25]};
      turning = false;
    }

    private IEnumerator MPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[7], cubieData[2], cubieData[3], cubieData[15], cubieData[5], cubieData[6], cubieData[24], cubieData[8], cubieData[9], cubieData[4], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[21], cubieData[16], cubieData[17], cubieData[1], cubieData[19], cubieData[20], cubieData[10], cubieData[22], cubieData[23], cubieData[18], cubieData[25]};
      turning = false;
    }

    private IEnumerator E() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[14], cubieData[12], cubieData[9], cubieData[15], cubieData[10], cubieData[16], cubieData[13], cubieData[11], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator EPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[11], cubieData[13], cubieData[16], cubieData[10], cubieData[15], cubieData[9], cubieData[12], cubieData[14], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator S() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[5], cubieData[13], cubieData[22], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[4], cubieData[21], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[3], cubieData[12], cubieData[20], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator SPrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[20], cubieData[12], cubieData[3], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[21], cubieData[4], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[22], cubieData[13], cubieData[5], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator RWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]], cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[7], cubieData[8], cubieData[3], cubieData[15], cubieData[16], cubieData[6], cubieData[24], cubieData[25], cubieData[9], cubieData[4], cubieData[5], cubieData[12], cubieData[13], cubieData[14], cubieData[21], cubieData[22], cubieData[17], cubieData[1], cubieData[2], cubieData[20], cubieData[10], cubieData[11], cubieData[23], cubieData[18], cubieData[19]};
      turning = false;
    }

    private IEnumerator RWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]], cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[13].position, Vector3.forward, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[18], cubieData[19], cubieData[3], cubieData[10], cubieData[11], cubieData[6], cubieData[1], cubieData[2], cubieData[9], cubieData[21], cubieData[22], cubieData[12], cubieData[13], cubieData[14], cubieData[4], cubieData[5], cubieData[17], cubieData[24], cubieData[25], cubieData[20], cubieData[15], cubieData[16], cubieData[23], cubieData[7], cubieData[8]};
      turning = false;
    }

    private IEnumerator LWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]], cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[12].position, Vector3.forward, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[6], cubieData[7], cubieData[2], cubieData[14], cubieData[15], cubieData[5], cubieData[23], cubieData[24], cubieData[8], cubieData[3], cubieData[4], cubieData[11], cubieData[12], cubieData[13], cubieData[20], cubieData[21], cubieData[16], cubieData[0], cubieData[1], cubieData[19], cubieData[9], cubieData[10], cubieData[22], cubieData[17], cubieData[18], cubieData[25]};
      turning = false;
    }

    private IEnumerator LWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]], cubies[cubieData[1]], cubies[cubieData[4]], cubies[cubieData[7]], cubies[cubieData[10]], cubies[cubieData[15]], cubies[cubieData[18]], cubies[cubieData[21]], cubies[cubieData[24]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[12].position, Vector3.forward, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[17], cubieData[18], cubieData[2], cubieData[9], cubieData[10], cubieData[5], cubieData[0], cubieData[1], cubieData[8], cubieData[20], cubieData[21], cubieData[11], cubieData[12], cubieData[13], cubieData[3], cubieData[4], cubieData[16], cubieData[23], cubieData[24], cubieData[19], cubieData[14], cubieData[15], cubieData[22], cubieData[6], cubieData[7], cubieData[25]};
      turning = false;
    }

    private IEnumerator UWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[21].position, Vector3.up, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[11], cubieData[13], cubieData[16], cubieData[10], cubieData[15], cubieData[9], cubieData[12], cubieData[14], cubieData[19], cubieData[22], cubieData[25], cubieData[18], cubieData[21], cubieData[24], cubieData[17], cubieData[20], cubieData[23]};
      turning = false;
    }

    private IEnumerator UWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[21].position, Vector3.up, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[14], cubieData[12], cubieData[9], cubieData[15], cubieData[10], cubieData[16], cubieData[13], cubieData[11], cubieData[23], cubieData[20], cubieData[17], cubieData[24], cubieData[21], cubieData[18], cubieData[25], cubieData[22], cubieData[19]};
      turning = false;
    }

    private IEnumerator DWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[6], cubieData[3], cubieData[0], cubieData[7], cubieData[4], cubieData[1], cubieData[8], cubieData[5], cubieData[2], cubieData[14], cubieData[12], cubieData[9], cubieData[15], cubieData[10], cubieData[16], cubieData[13], cubieData[11], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator DWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[4].position, Vector3.up, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[2], cubieData[5], cubieData[8], cubieData[1], cubieData[4], cubieData[7], cubieData[0], cubieData[3], cubieData[6], cubieData[11], cubieData[13], cubieData[16], cubieData[10], cubieData[15], cubieData[9], cubieData[12], cubieData[14], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator FWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[2], cubieData[11], cubieData[19], cubieData[5], cubieData[13], cubieData[22], cubieData[6], cubieData[7], cubieData[8], cubieData[1], cubieData[10], cubieData[18], cubieData[4], cubieData[21], cubieData[14], cubieData[15], cubieData[16], cubieData[0], cubieData[9], cubieData[17], cubieData[3], cubieData[12], cubieData[20], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator FWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[10].position, Vector3.right, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[17], cubieData[9], cubieData[0], cubieData[20], cubieData[12], cubieData[3], cubieData[6], cubieData[7], cubieData[8], cubieData[18], cubieData[10], cubieData[1], cubieData[21], cubieData[4], cubieData[14], cubieData[15], cubieData[16], cubieData[19], cubieData[11], cubieData[2], cubieData[22], cubieData[13], cubieData[5], cubieData[23], cubieData[24], cubieData[25]};
      turning = false;
    }

    private IEnumerator BWide() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
          obj.transform.RotateAround(cubieInit[15].position, Vector3.right, (90 - rot > moveSpeed) ? moveSpeed : 90 - rot);
        }
        rot += moveSpeed;
        yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[20], cubieData[12], cubieData[3], cubieData[23], cubieData[14], cubieData[6], cubieData[9], cubieData[10], cubieData[11], cubieData[21], cubieData[4], cubieData[24], cubieData[15], cubieData[7], cubieData[17], cubieData[18], cubieData[19], cubieData[22], cubieData[13], cubieData[5], cubieData[25], cubieData[16], cubieData[8]};
      turning = false;
    }

    private IEnumerator BWidePrime() {
      turning = true;
      GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[12]], cubies[cubieData[13]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]]};
      float rot = 0;
      while (rot < 90) {
        foreach (GameObject obj in affected) {
            obj.transform.RotateAround(cubieInit[15].position, Vector3.right, (90 - rot > moveSpeed) ? -moveSpeed : -90 + rot);
          }
          rot += moveSpeed;
          yield return new WaitForFixedUpdate();
      }
      cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[5], cubieData[13], cubieData[22], cubieData[8], cubieData[16], cubieData[25], cubieData[9], cubieData[10], cubieData[11], cubieData[4], cubieData[21], cubieData[7], cubieData[15], cubieData[24], cubieData[17], cubieData[18], cubieData[19], cubieData[3], cubieData[12], cubieData[20], cubieData[6], cubieData[14], cubieData[23]};
      turning = false;
    }
}
