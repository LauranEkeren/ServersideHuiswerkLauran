using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;
public class IdentitySeedData : ISeedData
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentitySeedData(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task EnsurePopulated(bool dropExisting = false)
    {
        const string CLAIMNAME_USERTYPE = "UserType";
        
        const string PASSWORD = "Secret123$";
        
        const string USERNAME_ADMIN = "admin";
        const string USERNAME_REGULARUSER = "user";
        const string USERNAME_EVENTOWNER = "eventowner";
        const string USERNAME_LIMITEDUSER = "limiteduser";

        
        //var claimUser = new Claim("User", "true");
        //var claimAdmin = new Claim("Admin", "true");
        //var claimEventOwner = new Claim("EventOwner", "true");


        if (dropExisting)
        {
            var existingUser = await _userManager.FindByNameAsync(USERNAME_ADMIN);
            if (existingUser != null)
                await _userManager.DeleteAsync(existingUser);

            existingUser = await _userManager.FindByNameAsync(USERNAME_REGULARUSER);
            if ( existingUser != null )
                await _userManager.DeleteAsync(existingUser);

            existingUser = await _userManager.FindByNameAsync(USERNAME_EVENTOWNER);
            if (existingUser != null)
                await _userManager.DeleteAsync(existingUser);

            existingUser = await _userManager.FindByIdAsync(USERNAME_LIMITEDUSER);
            if (existingUser != null)
            {
                await _userManager.DeleteAsync(existingUser);
            }
        }

        IdentityUser adminUser = await _userManager.FindByIdAsync(USERNAME_ADMIN);
        if (adminUser == null)
        {
            adminUser = new IdentityUser(USERNAME_ADMIN);
            
            await _userManager.CreateAsync(adminUser, PASSWORD);
            //await _userManager.AddClaimAsync(adminUser, claimAdmin);
            await _userManager.AddClaimAsync(adminUser, new Claim(CLAIMNAME_USERTYPE, "admin"));
        }

        IdentityUser regularUser = await _userManager.FindByIdAsync(USERNAME_REGULARUSER);
        if (regularUser == null)
        {
            regularUser = new IdentityUser(USERNAME_REGULARUSER);
            
            await _userManager.CreateAsync(regularUser, PASSWORD);
            //await _userManager.AddClaimAsync(regularUser, claimUser);
            await _userManager.AddClaimAsync(regularUser, new Claim(CLAIMNAME_USERTYPE, "user"));
        }

        IdentityUser eventOwnerUser = await _userManager.FindByIdAsync(USERNAME_EVENTOWNER);
        if (eventOwnerUser == null)
        {
            eventOwnerUser = new IdentityUser(USERNAME_EVENTOWNER);

            await _userManager.CreateAsync(eventOwnerUser, PASSWORD);
            //await _userManager.AddClaimAsync(eventOwnerUser, claimUser);
            //await _userManager.AddClaimAsync(eventOwnerUser, claimEventOwner);
            await _userManager.AddClaimAsync(eventOwnerUser, new Claim(CLAIMNAME_USERTYPE, "eventowner"));
        }


        IdentityUser limitedUser = await _userManager.FindByIdAsync(USERNAME_LIMITEDUSER);
        if (limitedUser == null)
        {
            limitedUser = new IdentityUser(USERNAME_LIMITEDUSER);

            await _userManager.CreateAsync(limitedUser, PASSWORD);
            //await _userManager.AddClaimAsync(limitedUser, claimLimitedUser);
            await _userManager.AddClaimAsync(limitedUser, new Claim(CLAIMNAME_USERTYPE, "limiteduser"));
        }

    }
}
