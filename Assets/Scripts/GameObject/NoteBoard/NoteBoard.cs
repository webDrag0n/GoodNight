using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBoard : MonoBehaviour
{
    public Animator note_board_animator;
    private void OnMouseOver()
    {
        note_board_animator.SetBool("out", true);
    }

    private void OnMouseExit()
    {
        note_board_animator.SetBool("out", false);
    }
}
