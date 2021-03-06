﻿namespace CodeStage.AdvancedFPSCounter.Label
{
	/// <summary>
	/// Anchor, used to stick counters labels to the screen corners.
	/// </summary>
	public enum LabelAnchor: byte
	{
		/// <summary>
		/// Upper left sceen corner. Text will be left-aligned.
		/// </summary>
		UpperLeft,

		/// <summary>
		/// Upper right sceen corner. Text will be right-aligned.
		/// </summary>
		UpperRight,

		/// <summary>
		/// Lower left sceen corner. Text will be left-aligned.
		/// </summary>
		LowerLeft,

		/// <summary>
		/// Lower right sceen corner. Text will be right-aligned.
		/// </summary>
		LowerRight
	}
}