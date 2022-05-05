namespace SocNetMockup.Data
{
    public static class AppRole
    {
        /// <summary>
        /// High administration rank. Can do virtually everything.
        /// </summary>
        public static readonly string Privilege_Admin = "Admin";

        /// <summary>
        /// Middle rank. Can ban people on the social network. Has access to report system.
        /// </summary>
        public static readonly string Privilege_Moder = "Moder";

        /// <summary>
        /// This range includes Moder and Admin.
        /// </summary>
        public static readonly string PrivilegeRange_ModerAndHigher = $"{Privilege_Moder},{Privilege_Admin}";

        /// <summary>
        /// Lowest rank. Answers tech support questions. Can see private accounts and groups.
        /// </summary>
        public static readonly string Privilege_TechSupport = "TechSupport";

        /// <summary>
        /// This range includes TechSupport, Moder and Admin.
        /// </summary>
        public static readonly string PrivilegeRange_TechSupportAndHigher = $"{Privilege_TechSupport},{PrivilegeRange_ModerAndHigher}";
    }
}
