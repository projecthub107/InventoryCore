using InventoryAPI.Data;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Controllers;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Repository;
using InventoryAPI.Services.IServices;
using InventoryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject Dbcontext
builder.Services.AddDbContext<InventoryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDBConnectionString")));


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IClientInfoRepository, ClientInfoRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IPreferencesRepository, PreferencesRepository>();
builder.Services.AddScoped<IProductTransactionRepository, ProductTransactionRepository>();
builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleRightRepository, RoleRightRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IClientInfoService, ClientInfoService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IPreferencesService, PreferencesService>();
builder.Services.AddScoped<IProductTransactionService, ProductTransactionService>();
builder.Services.AddScoped<IQuestionsService, QuestionsService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRightService, RoleRightService>();
builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();

builder.Services.AddCors((setup) =>
{
    setup.AddPolicy(("default"), (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
