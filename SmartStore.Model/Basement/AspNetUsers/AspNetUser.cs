using SmartStore.Model.Entities.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
    [Serializable]
    [DataContract(IsReference = true)]
    public partial class AspNetUser : EntityBase<string>
    {
        #region Primitive Properties

        [DataMember]
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        protected string _email;

        [DataMember]
        public virtual bool EmailConfirmed
        {
            get { return _emailConfirmed; }
            set
            {
                _emailConfirmed = value;
            }
        }
        protected bool _emailConfirmed;

        [DataMember]
        public virtual string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                _passwordHash = value;
            }
        }
        protected string _passwordHash;

        [DataMember]
        public virtual string SecurityStamp
        {
            get { return _securityStamp; }
            set
            {
                _securityStamp = value;
            }
        }
        protected string _securityStamp;

        [DataMember]
        public virtual string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
            }
        }
        protected string _phoneNumber;

        [DataMember]
        public virtual bool PhoneNumberConfirmed
        {
            get { return _phoneNumberConfirmed; }
            set
            {
                _phoneNumberConfirmed = value;
            }
        }
        protected bool _phoneNumberConfirmed;

        [DataMember]
        public virtual bool TwoFactorEnabled
        {
            get { return _twoFactorEnabled; }
            set
            {
                _twoFactorEnabled = value;
            }
        }
        protected bool _twoFactorEnabled;

        [DataMember]
        public virtual Nullable<System.DateTime> LockoutEndDateUtc
        {
            get { return _lockoutEndDateUtc; }
            set
            {
                _lockoutEndDateUtc = value;
            }
        }
        protected Nullable<System.DateTime> _lockoutEndDateUtc;

        [DataMember]
        public virtual bool LockoutEnabled
        {
            get { return _lockoutEnabled; }
            set
            {
                _lockoutEnabled = value;
            }
        }
        protected bool _lockoutEnabled;

        [DataMember]
        public virtual int AccessFailedCount
        {
            get { return _accessFailedCount; }
            set
            {
                _accessFailedCount = value;
            }
        }
        protected int _accessFailedCount;

        [DataMember]
        public virtual string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }
        protected string _userName;

        [DataMember]
        public virtual Nullable<bool> IsLocked
        {
            get { return _isLocked; }
            set
            {
                _isLocked = value;
            }
        }
        protected Nullable<bool> _isLocked;

        [DataMember]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }
        protected string _description;

        [DataMember]
        public virtual byte[] Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
            }
        }
        protected byte[] _avatar;

        [DataMember]
        public virtual Nullable<int> CenterId
        {
            get { return _centerId; }
            set
            {
                _centerId = value;
            }
        }
        protected Nullable<int> _centerId;

        [DataMember]
        public virtual Nullable<int> GroupId
        {
            get { return _groupId; }
            set
            {
                _groupId = value;
            }
        }
        protected Nullable<int> _groupId;
        
        [DataMember]
        public virtual Nullable<System.DateTime> LastLoginDate
        {
            get { return _lastLoginDate; }
            set
            {
                _lastLoginDate = value;
            }
        }
        protected Nullable<System.DateTime> _lastLoginDate;

        [DataMember]
        public virtual Nullable<System.DateTime> LastLoginDatePreview
        {
            get { return _lastLoginDatePreview; }
            set
            {
                _lastLoginDatePreview = value;
            }
        }
        protected Nullable<System.DateTime> _lastLoginDatePreview;

        [DataMember]
        public virtual Nullable<System.DateTime> LastPasswordChangedDate
        {
            get { return _lastPasswordChangedDate; }
            set
            {
                _lastPasswordChangedDate = value;
            }
        }
        protected Nullable<System.DateTime> _lastPasswordChangedDate;

        #endregion
      
    }
}
