using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADetector : MonoBehaviour
{
    public float visualDetectionDistance = 10f;
    public float visualDetectionAngle = 60f;
    public float peripheraDetectionRaduis = 5f;
    public float audioDetectionRaduis = 10f;
    [Space]
    public LayerMask visualDetectionLayer;
    public LayerMask peripheraDetectionLayer;
    public LayerMask audioDetectionLayer;

    void Update()
    {
        VisualDetection();
        PeripheraDetection();
        AudioDetection();
    }

    private void VisualDetection()
    {
        Collider[] visualColliders = Physics.OverlapSphere(transform.position, visualDetectionDistance, visualDetectionLayer);

        foreach (Collider collider in visualColliders)
        {
            Vector3 diretionToTarget = collider.transform.position - transform.position;
            diretionToTarget.y = 0f;

            float angleToTarget = Vector3.Angle(transform.forward, diretionToTarget);

            if (angleToTarget <= visualDetectionAngle / 2)
            {
                Debug.Log("Object détecté visuellement: " + collider.name);
            }
        }
    }

    private void PeripheraDetection()
    {
        Collider[] peripheralColliders = Physics.OverlapSphere(transform.position, peripheraDetectionRaduis, peripheraDetectionLayer);

        foreach (Collider collider in peripheralColliders)
        {
            Debug.Log("Object détecté Peripheriquement: " + collider.name);
        }
    }

    private void AudioDetection()
    {
        Collider[] audioColliders = Physics.OverlapSphere(transform.position, audioDetectionRaduis, audioDetectionLayer);

        foreach (Collider collider in audioColliders)
        {
            Debug.Log("Object détecté audiovisuellement: " + collider.name);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 coneStart = transform.position + transform.up * 0.5f;
        Quaternion coneRotation = Quaternion.LookRotation(transform.forward, transform.up);
        Vector3 coneDirection = coneRotation * Vector3.forward;
        float coneLength = visualDetectionDistance;
        float coneRadius = Mathf.Tan(visualDetectionAngle * 0.5f * Mathf.Deg2Rad) * coneLength;
        Vector3 coneEnd1 = coneStart + coneDirection * coneLength * 0.5f + transform.right * coneRadius;
        Vector3 coneEnd2 = coneStart + coneDirection * coneLength * 0.5f - transform.right * coneRadius;
        Gizmos.DrawLine(coneStart, coneEnd1);
        Gizmos.DrawLine(coneStart, coneEnd2);
        Gizmos.DrawLine(coneEnd1, coneEnd2);
        Gizmos.DrawWireSphere(coneStart, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, peripheraDetectionRaduis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, audioDetectionRaduis);
    }
}
