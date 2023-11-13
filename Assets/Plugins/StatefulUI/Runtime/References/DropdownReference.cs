using System;
using StatefulUI.Runtime.Core;
using StatefulUI.Runtime.RoleAttributes;
using TMPro;
using UnityEngine.UI;

namespace StatefulUI.Runtime.References
{



	[Serializable]
	public class DropdownReference : BaseReference
	{
		[Role(typeof(DropdownRoleAttribute), "Copy Universal Link", "CopyUniversalLink")]
		public int Role;
		public TMP_Dropdown DropdownFieldTMP;
		public Dropdown DropdownField;
		public bool IsTMP;

		public int Value
		{
			get => IsTMP ? DropdownFieldTMP.value : DropdownField.value;
			set
			{
				if (IsTMP)
				{
					DropdownFieldTMP.value = value;
				}
				else
				{
					DropdownField.value = value;
				}
			}
		}


		public bool IsEmpty => DropdownFieldTMP == null && DropdownField == null;

		public void SetValue(int value)
		{
			if (IsTMP) DropdownFieldTMP.value = value;
			else DropdownField.value = value;
		}

		public void SetActive(bool isActive)
		{
			if (IsTMP) DropdownFieldTMP.gameObject.SetActive(isActive);
			else DropdownField.gameObject.SetActive(isActive);
		}
	}
}
