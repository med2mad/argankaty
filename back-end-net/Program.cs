using back_end_net.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));

//builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAll");
app.UseStaticFiles();

//app.UseAuthorization(); // Uncomment if you use authorization

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<DBContext>();
    context.Database.Migrate();

    if (!context.products.Any()) {
        context.products.Add(new Product {
            nameEN = "Thyme Honey - 250 g",
            nameFR = "Miel de Thym - 250 g",
            descriptionEN = "Soothes coughs, aids digestion, promotes wound healing, and supports respiratory health.",
            descriptionFR = "Apaise la toux, facilite la digestion, favorise la cicatrisation des plaies et soutient la respiration.",
            price = 149,
            photo = "4.jpg",
            descriptionCH = "舒缓咳嗽、帮助消化、促进伤口愈合并支持呼吸系统健康。",
            nameCH = "摩洛哥有机蜂蜜 - 250 g",
            order = 7,
            promo = 0,
            type = "Food"
        });
        context.products.Add(new Product {
            nameEN = "Eucalyptus Honey - 250 g",
            type = "Food",
            descriptionEN = "Soothes coughs and calms sore throats. It boosts digestion, healing wounds, and energy flow.",
            price = 129,
            promo = 10,
            photo = "3.jpg",
            nameFR = "Miel d'Eucalyptus - 250 g",
            nameCH = "摩洛哥有机蜂蜜 - 250 g",
            descriptionFR = "Apaise la toux, le rhume et maux de gorge, stimule la digestion, la cicatrisation des plaies.",
            descriptionCH = "舒缓咳嗽，缓解咽喉痛。促进消化，促进伤口愈合，促进能量流动。",
            order = 8
        });
        context.products.Add(new Product {
            nameEN = "Olive Oil- 250 ml",
            type = "Food / Beauty",
            descriptionEN = "Rich flavored with health benefits. Offers a versatile choice for cooking, and skincare.",
            price = 69,
            promo = 15,
            photo = "55.jpg",
            nameFR = "Huile d'olive - 250 ml",
            nameCH = "橄榄油- 250 ML",
            descriptionFR = "Saveur riche et bienfaits pour la santé. Un choix polyvalent pour la cuisine et les soins de la peau.",
            descriptionCH = "口味浓郁，健康益处多多。烹饪、护肤皆宜，用途广泛。",
            order = 1
        });
        context.products.Add(new Product {
            nameEN = "Argan Oil Edible - 250 ml",
            type = "Food",
            descriptionEN = "Packed with vitamin E and fatty acids. Supports heart health, boosts immunity and digestion.",
            price = 450,
            promo = 25,
            photo = "5.png",
            nameFR = "Huile d'argan comestible - 250 ml",
            nameCH = "食用摩洛哥坚果油 - 250 ml",
            descriptionFR = "Riche en vitamine E et acides gras. Favorise la santé cardiaque, renforce l'immunité et digestion.",
            descriptionCH = "富含维生素E和脂肪酸。有益于心脏健康，增强免疫力和消化功能。",
            order = 3
        });
        context.products.Add(new Product {
            nameEN = "Argan Oil Cosmetic - 50 ml",
            type = "Health / Beauty",
            descriptionEN = "Slows down skin aging and gives a radiant appearance. Gives hair smooths split ends.",
            price = 129,
            promo = 0,
            photo = "1.png",
            nameFR = "Huile d'Argan Cosmétique - 50 ml",
            nameCH = "阿甘油 - 50 ml",
            descriptionFR = "Ralentit le vieillissement cutané et donne un aspect radieux. Lisse les cheveux et les pointes fourchues.",
            descriptionCH = "延缓皮肤衰老，令肌肤容光焕发。抚平头发分叉。",
            order = 2
        });
        context.products.Add(new Product {
            nameEN = "Safran",
            type = "Food",
            descriptionEN = "Aromatic flavor. Hand-harvested and pure, it’s perfect for culinary, medicinal, and cosmetic uses.",
            price = 69,
            promo = 0,
            photo = "56.jpg",
            nameFR = "Safran",
            nameCH = "摩洛哥藏红花",
            descriptionFR = "Saveur aromatique. Récolté à la main et pur. Pour les usages culinaires, médicinaux et cosmétiques.",
            descriptionCH = "香气浓郁。手工采摘，口感纯净，非常适合烹饪、药用和美容用途。",
            order = 4
        });
        context.products.Add(new Product {
            nameEN = "Black Soap",
            type = "Cleaning",
            descriptionEN = "Is a traditional skincare product. It leaves skin soft, smooth, and rejuvenated.",
            price = 39,
            promo = 0,
            photo = "57.png",
            nameFR = "Savon noir",
            nameCH = "有机黑非洲",
            descriptionFR = "C'est un soin traditionnel qui laisse la peau douce, lisse et rajeunie.",
            descriptionCH = "是一款传统的护肤产品。它能使肌肤柔软、光滑、焕发活力。",
            order = 9
        });
        context.products.Add(new Product {
            nameEN = "Amlou - 200 g",
            type = "Food",
            descriptionEN = "Traditional Moroccan sweet spread from roasted almonds, argan oil and honey. Rich and nutty.",
            price = 100,
            promo = 15,
            photo = "58.png",
            nameFR = "Amlou - 200 g",
            nameCH = "Amlou - 200 G",
            descriptionFR = "Pâte à tartiner à base d'amandes grillées, miel et d'huile d'argan. Riche et parfumée aux noisettes.",
            descriptionCH = "传统的摩洛哥甜点，由烤杏仁、摩洛哥坚果油和蜂蜜调制而成。口感浓郁，坚果香浓郁。",
            order = 5
        });
        context.products.Add(new Product {
            nameEN = "Vervain",
            type = "Health",
            descriptionEN = "Reduce Anxiety and depression.",
            price = 39,
            promo = 0,
            photo = "59.png",
            nameFR = "Verveine",
            nameCH = "马鞭草",
            descriptionFR = "Réduire l’anxiété et la dépression.",
            descriptionCH = "减轻焦虑和抑郁。",
            order = 6
        });
        context.SaveChanges();
    }
}

app.Run();
