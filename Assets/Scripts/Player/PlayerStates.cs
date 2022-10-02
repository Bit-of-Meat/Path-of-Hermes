namespace Player.States {
    /// <summary>
    /// All PlayerStates
    /// </summary>
    public enum PlayerStates {
        /// <summary>Runs when the player is on the ground</summary>
        Ground,
        /// <summary>Executed when the player presses the jump button</summary>
        Jump,
        /// <summary>Runs when the player is in the "Wallrun" state</summary>
        Walljump,
        /// <summary>Fired when the player has left the surface or the jump state has ended</summary>
        Fall,
        /// <summary>Performed while running on the wall</summary>
        Wallrun,
        /// <summary>All PlayerStates</summary>
        Swing,
        /// <summary>All PlayerStates</summary>
        Hook,
        /// <summary>Runs when the player is on the ground and not moving</summary>
        Idle,
        /// <summary>Executed when the player presses the WASD buttons</summary>
        Walk,
        /// <summary>Fired when the player is in walking state and the run button is pressed</summary>
        Run,
        /// <summary>Only runs when the player is on the ground and the crouch button is pressed</summary>
        Crouch,
        /// <summary>Substate of Ground Performed while the player makes a tackle</summary>
        Slide
    }
}