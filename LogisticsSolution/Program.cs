//...

app.UseRouting();

app.UseCors("SignalRPolicy");

// app.UseHttpsRedirection(); // REMOVE or COMMENT this for container-only environments

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok("Healthy")); // ✅ ADD THIS

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notification");
});

app.Run();
