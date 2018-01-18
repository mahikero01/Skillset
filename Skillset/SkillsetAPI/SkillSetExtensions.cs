﻿using SkillsetAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsetAPI
{

    public static class SkillSetExtensions
    {
        public static void EnsureSeedDataForContext(this SkillSetContext ctx)
        {
            SeedSetUsers(ctx);
            SeedSetGroups(ctx);
            SeedSetUserAccess(ctx);
            SeedSetModules(ctx);
            SeedSetGroupAccess(ctx);
        }

        private static void SeedSetUsers(SkillSetContext ctx)
        {
            if (ctx.SetUsers.Any())
            {
                return;
            }

            var setUsers = new List<SetUser>()
                    {
                        new SetUser()
                        {
                            user_id = "USER-20150428-001",
                            user_name = "sarmife",
                            user_last_name = "Sarmiento",
                            user_first_name = "Federico",
                            user_middle_name = "Paras",
                            can_PROD = false,
                            can_UAT = false,
                            can_PEER = false,
                            can_DEV = false,
                            created_date = new DateTime(2015,04,28,19,05,40)
                        },
                        new SetUser()
                        {
                            user_id = "USER-20171128-002",
                            user_name = "alvarer",
                            user_last_name = "Alvarez",
                            user_first_name = "Eros",
                            user_middle_name = "K",
                            can_PROD = false,
                            can_UAT = false,
                            can_PEER = false,
                            can_DEV = false,
                            created_date = new DateTime(2017,11,28,19,05,40)
                        }
                    };

            ctx.SetUsers.AddRange(setUsers);
            ctx.SaveChanges();
        }

        private static void SeedSetGroups(SkillSetContext ctx)
        {
            if (ctx.SetGroups.Any())
            {
                return;
            }

            var setGroups = new List<SetGroup>()
                    {
                        new SetGroup()
                        {
                            grp_id = "GRP-20150428-001",
                            grp_name = "Admin",
                            grp_desc = "Super user of Skillset",
                            created_date = new DateTime(2015,04,28,19,05,40)
                        },
                        new SetGroup()
                        {
                            grp_id = "GRP-20150428-002",
                            grp_name = "Limited",
                            grp_desc = "Limited user",
                            created_date = new DateTime(2015,04,28,19,05,40)
                        }
                    };

            ctx.SetGroups.AddRange(setGroups);
            ctx.SaveChanges();
        }

        private static void SeedSetUserAccess(SkillSetContext ctx)
        {
            if (ctx.SetUserAccesses.Any())
            {
                return;
            }

            var setUserAccess = new List<SetUserAccess>()
                    {
                        new SetUserAccess()
                        {
                            grp_id = "GRP-20150428-001",
                            user_id ="USER-20150428-001"
                        },
                        new SetUserAccess()
                        {
                            grp_id = "GRP-20150428-002",
                            user_id ="USER-20171128-002"
                        }
                    };

            ctx.SetUserAccesses.AddRange(setUserAccess);
            ctx.SaveChanges();
        }

        private static void SeedSetModules(SkillSetContext ctx)
        {
            if (ctx.SetModules.Any())
            {
                return;
            }

            var setModules = new List<SetModule>()
                    {
                        new SetModule()
                        {
                            mod_id = "MOD-20150428-001",
                            mod_name = "Skillset",
                            mod_desc = "Main Dashboard",
                            created_date = new DateTime(2015,04,28,19,05,40)
                        },
                        new SetModule()
                        {
                            mod_id = "MOD-20150428-002",
                            mod_name = "Maintenace",
                            mod_desc = "Maintenance module",
                            created_date = new DateTime(2015,04,28,19,05,40)
                        }
                    };

            ctx.SetModules.AddRange(setModules);
            ctx.SaveChanges();
        }

        private static void SeedSetGroupAccess(SkillSetContext ctx)
        {
            if (ctx.SetGroupAccesses.Any())
            {
                return;
            }

            var setGroupAccesses = new List<SetGroupAccess>()
                    {
                        new SetGroupAccess()
                        {
                            grp_id = "GRP-20150428-001",
                            mod_id = "MOD-20150428-001",
                            can_view = true,
                            can_add = false,
                            can_edit = false,
                            can_delete = false
                        }
                    };

            ctx.SetGroupAccesses.AddRange(setGroupAccesses);
            ctx.SaveChanges();
        }
    }
}