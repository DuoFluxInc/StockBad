using System;
using System.Text;
using CodeStage.AdvancedFPSCounter.Label;
using UnityEngine;

#if UNITY_FLASH
using UnityEngine.Flash;
#endif

namespace CodeStage.AdvancedFPSCounter.CountersData
{
	/// <summary>
	/// Shows memory usage data.
	/// </summary>
	[Serializable]
	public class MemoryCounterData
	{
		private const string COROUTINE_NAME = "UpdateMemoryCounter";
		private const string TEXT_START = "<color=#{0}><b>";
		private const string LINE_START_TOTAL = "MEM (total): ";
		private const string LINE_START_ALLOCATED = "MEM (alloc): ";
		private const string LINE_START_MONO = "MEM (mono): ";
		private const string LINE_END = " MB";
		private const string TEXT_END = "</b></color>";
		private const int MEMORY_DIVIDER = 1048576; // 1024^2

		[SerializeField]
		private bool enabled = true;

		[SerializeField]
		[Range(0.1f, 10f)]
		private float updateInterval = 1f;

		[SerializeField]
		private LabelAnchor anchor = LabelAnchor.UpperLeft;

		[SerializeField]
		private bool preciseValues = false;

		[SerializeField]
		private Color color = new Color32(234, 238, 101, 255);
		private string colorCached;

		[SerializeField]
		private bool totalReserved = true;

		[SerializeField]
		private bool allocated = true;

		[SerializeField]
		private bool monoUsage = true;

		internal StringBuilder text;
		internal bool dirty = false;
		private bool inited = false;

#if !UNITY_FLASH
		private uint previousTotalValue = 0;
		private uint previousAllocatedValue = 0;
		private long previousMonoValue = 0;
#else
		private int previousTotalValue = 0;
#endif

		/// <summary>
		/// Enables or disables counter with immediate label refresh.
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled == value || !Application.isPlaying) return;

				enabled = value;

				if (value)
				{
					if (!inited && (HasData())) Init();
				}
				else
				{
					if (inited) Uninit();
				}

				AFPSCounter.Instance.UpdateTexts();
			}
		}

		/// <summary>
		/// Counter's value update interval.
		/// </summary>
		public float UpdateInterval
		{
			get { return updateInterval; }
			set
			{
				if (updateInterval == value || !Application.isPlaying) return;
				updateInterval = value;
				if (!enabled) return;

				RestartCoroutine();
			}
		}

		/// <summary>
		/// Changes counter's label. Refreshes both previous and current label.
		/// </summary>
		public LabelAnchor Anchor
		{
			get
			{
				return anchor;
			}
			set
			{
				if (anchor == value || !Application.isPlaying) return;
				LabelAnchor prevAnchor = anchor;
				anchor = value;
				if (!enabled) return;

				dirty = true;
				AFPSCounter.Instance.MakeDrawableLabelDirty(prevAnchor);
				AFPSCounter.Instance.UpdateTexts();	
			}
		}

		/// <summary>
		/// Allows to output memory usage more precisely thus using more system resources.
		/// </summary>
		public bool PreciseValues
		{
			get { return preciseValues; }
			set
			{
				if (preciseValues == value || !Application.isPlaying) return;
				preciseValues = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Color of the memory counter.
		/// </summary>
		public Color Color
		{
			get { return color; }
			set
			{
				if (color == value || !Application.isPlaying) return;
				color = value;
				if (!enabled) return;

				colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
				Refresh();
			}
		}

		/// <summary>
		/// Allows to see total reserved memory size. Also used to see private memory usage in Flash Player.
		/// </summary>
		public bool TotalReserved
		{
			get { return totalReserved; }
			set
			{
				if (totalReserved == value || !Application.isPlaying) return;
				totalReserved = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see amount of currently allocated memory.
		/// </summary>
		public bool Allocated
		{
			get { return allocated; }
			set
			{
				if (allocated == value || !Application.isPlaying) return;
				allocated = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see memory amount used by managed Mono objects.
		/// </summary>
		public bool MonoUsage
		{
			get { return monoUsage; }
			set
			{
				if (monoUsage == value || !Application.isPlaying) return;
				monoUsage = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Updates counter's value and forces label refresh.
		/// </summary>
		public void Refresh()
		{
			if (!enabled || !Application.isPlaying) return;
			UpdateValue(true);
			AFPSCounter.Instance.UpdateTexts();
		}

		internal void Init()
		{
			if (!enabled) return;
			if (!HasData()) return;

			inited = true;

			previousTotalValue = 0;
#if !UNITY_FLASH
			previousAllocatedValue = 0;
			previousMonoValue = 0;
#endif

			if (colorCached == null)
			{
				colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
			}

			if (text == null)
			{
				text = new StringBuilder(200);
			}
			else
			{
				text.Length = 0;
			}

			text.Append(colorCached);

			if (totalReserved)
			{
				if (preciseValues)
				{
					text.Append(LINE_START_TOTAL).AppendFormat("{0:F}", 0).Append(LINE_END);
				}
				else
				{
					text.Append(LINE_START_TOTAL).Append(0).Append(LINE_END);
				}
			}

			if (allocated)
			{
				if (text.Length > 0) text.Append(AFPSCounter.NEW_LINE);
				if (preciseValues)
				{
					text.Append(LINE_START_ALLOCATED).AppendFormat("{0:F}", 0).Append(LINE_END);
				}
				else
				{
					text.Append(LINE_START_ALLOCATED).Append(0).Append(LINE_END);
				}
			}

			if (monoUsage)
			{
				if (text.Length > 0) text.Append(AFPSCounter.NEW_LINE);
				if (preciseValues)
				{
					text.Append(LINE_START_MONO).AppendFormat("{0:F}", 0).Append(LINE_END);
				}
				else
				{
					text.Append(LINE_START_MONO).Append(0).Append(LINE_END);
				}
			}

			text.Append(TEXT_END);

			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);
		}

		internal void Uninit()
		{
			if (text != null)
			{
				text.Length = 0;
			}

			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);

			inited = false;
		}

		private void RestartCoroutine()
		{
			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
		}

		internal void UpdateValue(bool force = false)
		{
			if (!enabled) return;

			if (force)
			{
				if (!inited && (HasData()))
				{
					Init();
					return;
				}

				if (inited && (!HasData()))
				{
					Uninit();
					return;
				}
			}

			if (totalReserved)
			{
				
#if !UNITY_FLASH
				uint value = Profiler.GetTotalReservedMemory();
				uint divisionResult = 0;
#else
				int value;
				int divisionResult = 0;
	#if UNITY_EDITOR
				value = (int)Profiler.GetTotalReservedMemory();
	#else
				value = ActionScript.Expression<int>("GateFromFlashWorld.GetPrivateMemory();");
	#endif
#endif
				bool newValue;
				if (preciseValues)
				{
					newValue = (previousTotalValue != value);
				}
				else
				{
					divisionResult = value / MEMORY_DIVIDER;
					newValue = (previousTotalValue != divisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						previousTotalValue = value;
					}
					else
					{
						previousTotalValue = divisionResult;
					}

					dirty = true;
				}
			}

#if !UNITY_FLASH
			if (allocated)
			{
				uint value = Profiler.GetTotalAllocatedMemory();
				uint divisionResult = 0;

				bool newValue;
				if (preciseValues)
				{
					newValue = (previousAllocatedValue != value);
				}
				else
				{
					divisionResult = value / MEMORY_DIVIDER;
					newValue = (previousAllocatedValue != divisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						previousAllocatedValue = value;
					}
					else
					{
						previousAllocatedValue = divisionResult;
					}

					dirty = true;
				}
			}

			if (monoUsage)
			{
				long monoMemory = System.GC.GetTotalMemory(false);
				long monoDivisionResult = 0;

				bool newValue;
				if (preciseValues)
				{
					newValue = (previousMonoValue != monoMemory);
				}
				else
				{
					monoDivisionResult = monoMemory / MEMORY_DIVIDER;
					newValue = (previousMonoValue != monoDivisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						previousMonoValue = monoMemory;
					}
					else
					{
						previousMonoValue = monoDivisionResult;
					}
					
					dirty = true;
				}
			}
#endif

			if (dirty)
			{
#if !UNITY_FLASH
				bool needNewLine = false;
#endif

				text.Length = 0;
				text.Append(colorCached);

				if (totalReserved)
				{
					text.Append(LINE_START_TOTAL);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", previousTotalValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(previousTotalValue);
					}
					text.Append(LINE_END);

#if !UNITY_FLASH
					needNewLine = true;
#endif
				}

#if !UNITY_FLASH
				if (allocated)
				{
					if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
					text.Append(LINE_START_ALLOCATED);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", previousAllocatedValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(previousAllocatedValue);
					}
					text.Append(LINE_END);
					needNewLine = true;
				} 

				if (monoUsage)
				{
					if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
					text.Append(LINE_START_MONO);

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", previousMonoValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(previousMonoValue);
					}
					
					text.Append(LINE_END);
				}
#endif
				
				text.Append(TEXT_END);
			}
		}

		private bool HasData()
		{
			return totalReserved || allocated || monoUsage;
		}
	}
}