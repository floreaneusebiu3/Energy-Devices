import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "src/app/service/auth/auth.service";


@Injectable({
    providedIn: 'root'
})
export class AuthorizationGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        var token = this.authService.getToken();
        var role = this.authService.getRoleFromToken(token);
        if (!(role === "ADMIN")) {
            this.router.navigateByUrl("login");
            return false;
        }
        return true;
    }
}