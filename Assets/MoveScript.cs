using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private float timeTaken = 0.2f; // ����ɂ����鎞��
    private float timeElapsed; // �o�ߎ���
    private Vector3 destination; // �ړI�n
    private Vector3 origin; // �o���n�_

    private void Start()
    {
        destination = transform.position; // �����ʒu
        origin = destination; // �o���n�_�������ʒu�Ɠ���
    }

    public void MoveTo(Vector3 newDestination)
    {
        timeElapsed = 0; // �o�ߎ��Ԃ����Z�b�g
        origin = destination; // ���݂̖ړI�n���o���n�_�ɐݒ�
        destination = newDestination; // �V�����ړI�n��ݒ�
    }

    private void Update()
    {
        //if (origin == destination) return; // ���ɖړI�n�ɂ���ꍇ�͉������Ȃ�

        //timeElapsed += Time.deltaTime; // �o�ߎ��Ԃ𑝉�
        //float timeRate = timeElapsed / timeTaken; // ���Ԃ̊������v�Z

        //// timeRate��0����1�͈̔͂ɐ���
        //timeRate = Mathf.Clamp01(timeRate);

        //// �ʒu����
        //transform.position = Vector3.Lerp(origin, destination, timeRate);

        //// �ړI�n�ɓ��B���������m�F
        //if (timeRate >= 1)
        //{
        //    origin = destination; // �V���Ȉړ����J�n�����܂ōX�V���~
        //}

        // �ړI�n�ɓ������Ă����珈�����Ȃ�

        if (origin == destination) { return; }

        //���Ԍo�߂����Z
        timeElapsed += Time.deltaTime;
        
        // �o�ߎ��Ԃ��������Ԃ̉��������Z�o
        float timeRate = timeElapsed / timeTaken;
        
        // �������Ԃ𒴂���悤�ł���Ύ��s�������ԑ����Ɋۂ߂�B
        if (timeRate > 1) { timeRate = 1; }
        
        // �C�[�W���O�p�v�Z(���j�A)
        float easing = timeRate;
        
        //���W���Z�o
        Vector3 currentPosition = Vector3.Lerp(origin, destination, easing);
        
        // �Z�o�������W��position�ɑ��
        transform.position = currentPosition;
    }
}
