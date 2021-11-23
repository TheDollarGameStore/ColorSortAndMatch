using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [HideInInspector]
    public Sphere holding;

    [HideInInspector]
    public Tube targetTube;
    // Start is called before the first frame update

    public void PickupBall()
    {
        if (targetTube != null)
        {
            if (targetTube.spheres.Count > 0)
            {
                holding = targetTube.TakeBall();
                holding.inClaw = true;
            }
        }
    }

    // Update is called once per frame
    public void DropBall()
    {
        if (holding != null)
        {
            if (targetTube.spheres.Count < 5)
            {
                targetTube.AddBall(holding);
                holding.inClaw = false;
                holding = null;
            }
        }
    }

    private void Update()
    {
        if (targetTube != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(targetTube.gameObject.transform.position.x, transform.position.y, transform.position.z), 20f * Time.deltaTime);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tube"))
            {
                targetTube = hit.collider.gameObject.GetComponent<Tube>();
            }
        }

        //Controls
        if (GameManager.instance.canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (holding == null)
                {
                    PickupBall();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropBall();
            }

            if (Input.GetMouseButtonDown(1))
            {
                GameManager.instance.FillUp(3);
            }
        }
    }
}
