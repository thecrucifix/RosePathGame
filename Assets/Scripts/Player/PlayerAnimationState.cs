using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationState : PlayerMovement
{
    #region Variables

    private string State;
    public Animator animator;
    private string CurrentState;
    private PlayerMovement MovementStorage;
    private MeleeAttackManager CombatStorage;
    private Rigidbody2D rb;


    private bool isRunStart = false;
    private bool isRunMid;
    private bool isJumpEnd = false;
    private bool isFallEnd = false;
    private bool isLanding = false;
    private bool isFall;
    private bool PLAYER_IDLE;
    private bool PLAYER_RUN;
    private bool PLAYER_JUMP;
    private bool PLAYER_FALL;
    private bool PLAYER_ATTACKL;
    private bool PLAYER_ATTACKLAIR;
    private bool PLAYER_UPWARDATTACK_IDLE;
    private bool PLAYER_UPWARDATTACK_RUN;
    private bool PLAYER_UPWARDATTACK_JUMP;
    private bool PLAYER_DOWNWARDATTACK;
    private bool PLAYER_FORWARDATTACK;
    private bool PLAYER_FORWARDATTACK_RUN;
    private bool PLAYER_FORWARDATTACK_JUMP;
    private bool PLAYER_ISCROWLING_IDLE;
    private bool PLAYER_ISCROWLING_RUN;
    private bool PLAYER_ISCLIMBING;
    private bool PLAYER_ISCLIMBING_JUMP;

    #endregion

    #region MainMethods
    void Start()
    {
        animator = GetComponent<Animator>();
        MovementStorage = GetComponent<PlayerMovement>();
        CombatStorage = GetComponent<MeleeAttackManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(CurrentState);
        StateBox();
        StateMachine();
    }
    #endregion

    #region AnimationMethods

    void StateBox()
    {
        PLAYER_IDLE = MovementStorage.isIdle;
        PLAYER_RUN = MovementStorage.isRunning;
        PLAYER_FALL = MovementStorage.isFalling;
        PLAYER_JUMP = MovementStorage.isJumping;
        PLAYER_UPWARDATTACK_IDLE = CombatStorage.isUpwardAttackIdle;
        PLAYER_UPWARDATTACK_RUN = CombatStorage.isUpwardAttackRun;
        PLAYER_UPWARDATTACK_JUMP = CombatStorage.isUpwardAttackJump;
        PLAYER_DOWNWARDATTACK = CombatStorage.isDownwardAttack;
        PLAYER_FORWARDATTACK = CombatStorage.isForwardAttack;
        PLAYER_FORWARDATTACK_RUN = CombatStorage.isForwardAttackRun;
        PLAYER_FORWARDATTACK_JUMP = CombatStorage.isForwardAttackJump;
        PLAYER_ISCROWLING_IDLE = MovementStorage.isCrowlingIdle;
        PLAYER_ISCROWLING_RUN = MovementStorage.isCrowlingRun;
        PLAYER_ISCLIMBING = MovementStorage.isWallSliding;

    }
    void StateMachine()
    {
        #region MovementAnimations
        if (CombatStorage.meleeAttack == false)
        {
            if (PLAYER_ISCLIMBING)
            {
                animator.Play("Player_Climbing");
                CurrentState = ("Player_isClimbing");
                return;
            }

            if (PLAYER_ISCROWLING_IDLE)
            {
                animator.Play("Player_crow_idle");
                CurrentState = ("Player_isCrowling_idle");
                return;
            }

            if (PLAYER_ISCROWLING_RUN)
            {
                animator.Play("Player_Crow_Run");
                CurrentState = ("Player_isCrowlingRun");
                return;

            }

            if (PLAYER_IDLE)
            {
                animator.Play("Player_Idle");
                CurrentState = ("Idle");
                return;
            }

            if (PLAYER_RUN && !isRunStart && !isRunMid && MovementStorage.IsGrounded())
            {
                animator.Play("Player_Run_Start");
                CurrentState = ("Player_Run_Start");
                return;
            }

            if (PLAYER_RUN && isRunStart)
            {
                animator.Play("Player_Running");
                CurrentState = ("Run");
                isRunStart = false;
                isRunMid = true;
                return;
            }

            if (rb.velocity.x == 0 || !MovementStorage.IsGrounded())
            {
                isRunMid = false;
                isRunStart = false;
            }

            if (PLAYER_JUMP && !PLAYER_FALL)
            { 
                animator.Play("Player_Jump");
                CurrentState = ("Jump");
                return;
            }

            if (PLAYER_FALL && !MovementStorage.IsGrounded())
            {
                isFall = true;
                animator.Play("Player_Fall");
                CurrentState = ("Fall");
                return;
            }

            if (MovementStorage.IsGrounded())
            {
                isFall = false;
                isJumpEnd = false;
                isFallEnd = false;
            }
        }

    
        #endregion

        #region AttackAnimations

        if (PLAYER_UPWARDATTACK_IDLE)
        {
            CurrentState = ("Attack_Upward_Idle");
            animator.Play("Attack_Upward_Idle");
            return;

        }
        if (PLAYER_UPWARDATTACK_RUN)
        {
            CurrentState = ("Attack_Upward_Run");
            animator.Play("Attack_Upward_Run");
            return;
        }
        if (PLAYER_UPWARDATTACK_JUMP)
        {
            CurrentState = ("Attack_Upward_Jump");
            animator.Play("Attack_Upward_Jump");
            return;
        }
        if (PLAYER_DOWNWARDATTACK)
        {
            CurrentState = ("Attack_Downward_Fall");
            animator.Play("Attack_Downward_Fall");
            return;

        }
        if (PLAYER_FORWARDATTACK)
        {
            CurrentState = ("ForwardAttack");
            animator.Play("Forward_Attack");
            return;
        }
        if (PLAYER_FORWARDATTACK_RUN)
        {
            CurrentState = ("Attack_Forward_Run");
            animator.Play("Attack_Forward_Run");
            return;
        }
        if (PLAYER_FORWARDATTACK_JUMP)
        {
            CurrentState = ("Forward_Attack_Jummp");
            animator.Play("Attack_Forward_Jump");
            return;
        }

        if (CombatStorage.isForwardComboOne)
        {
            CurrentState = ("Combo Attack One");
            animator.Play("Forward_Attack2");
            return;
        }

        if (CombatStorage.isForwardComboTwo)
        {
            CurrentState = ("Combo Attack One");
            animator.Play("Forward_Attack3");
            return;
        }
        #endregion
    }

    #endregion

    #region TriggerAnimation

    public void runStart()
    {
        isRunStart = true;
    }

    public void fallPerformed()
    {
        isFallEnd = true;
        isLanding = true;
    }

    #endregion

}


